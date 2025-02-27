using Accelerate.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Accelerate.Logging
{
    /// <summary>
    /// Represent a type to perform logging based on file system.
    /// </summary>
    public sealed class DatabaseLoggerService : LoggerService<DatabaseLoggerServiceOptions>
    {
        private readonly IDbConnection _connection;
        private Boolean _disposed;
        private SemaphoreSlim _semaphore;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger service.
        /// </param>
        /// <param name="connection">
        /// Database connection.
        /// </param>
        public DatabaseLoggerService(IOptions<DatabaseLoggerServiceOptions> options, IDbConnection connection) : base(options)
        {
            _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

            if (connection.State == ConnectionState.Broken ||
                connection.State == ConnectionState.Closed)
            {
                throw new InvalidOperationException("Database connection has an invalid state");
            }

            _semaphore = new SemaphoreSlim(1, 1);
        }
        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger service.
        /// </param>
        /// <param name="categoryName">
        /// The category name for messages produced by the logger.
        /// </param>
        /// <param name="connection">
        /// Database connection.
        /// </param>
        public DatabaseLoggerService(IOptions<DatabaseLoggerServiceOptions> options, IDbConnection connection, String categoryName) : base(options, categoryName)
        {
            _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));

            if (connection.State == ConnectionState.Broken ||
                connection.State == ConnectionState.Closed)
            {
                throw new InvalidOperationException("Database connection has an invalid state");
            }

            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <inheritdoc />
        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            base.Dispose(disposing);

            if (disposing)
            {
                _semaphore.Dispose();
                _semaphore = null;
            }

            _disposed = true;
        }
        /// <summary>
        /// Insert an exception in database.
        /// </summary>
        /// <param name="dateTime">
        /// Exception datetime.
        /// </param>
        /// <param name="logLevel">
        /// Log level.
        /// </param>
        /// <param name="eventId">
        /// Event information.
        /// </param>
        /// <param name="exception">
        /// Exception information.
        /// </param>
        private void InsertException(DateTime dateTime, LogLevel logLevel, EventId eventId, Exception exception)
        {
            var queryString = "INSERT INTO exceptions VALUES (@dateTime, @appName, @categoryName, @logLevel, @eventId, @eventName, @message, @type, @stackTrace);";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = queryString;
                command.CommandTimeout = 5;
                command.CommandType = CommandType.Text;
                command.AddParameter("@appName", Options.AppName);
                command.AddParameter("@category", CategoryName);
                command.AddParameter("@dateTime", dateTime);
                command.AddParameter("@eventId", eventId.Id);
                command.AddParameter("@eventName", eventId.Name);
                command.AddParameter("@logLevel", logLevel);
                command.AddParameter("@message", exception.Message);
                command.AddParameter("@stackTrace", exception.StackTrace);
                command.AddParameter("@type", exception.GetType().Name);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Insert a trace in database.
        /// </summary>
        /// <param name="dateTime">
        /// Exception datetime.
        /// </param>
        /// <param name="logLevel">
        /// Log level.
        /// </param>
        /// <param name="eventId">
        /// Event information.
        /// </param>
        /// <param name="message">
        /// Message of trace.
        /// </param>
        private void InsertTrace(DateTime dateTime, LogLevel logLevel, EventId eventId, String message)
        {
            var queryString = "INSERT INTO traces VALUES (@dateTime, @appName, @categoryName, @logLevel, @eventId, @eventName, @message);";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = queryString;
                command.CommandTimeout = 5;
                command.CommandType = CommandType.Text;
                command.AddParameter("@appName", Options.AppName);
                command.AddParameter("@category", CategoryName);
                command.AddParameter("@dateTime", dateTime);
                command.AddParameter("@eventId", eventId.Id);
                command.AddParameter("@eventName", eventId.Name);
                command.AddParameter("@logLevel", logLevel);
                command.AddParameter("@message", message);
                command.ExecuteNonQuery();
            }
        }
        /// <inheritdoc />
        public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
        {
            Task.Run(() =>
            {
                if (IsEnabled(logLevel))
                {
                    var message = String.Empty;
                    var dateTime = DateTime.UtcNow;

                    if (formatter == null)
                    {
                        message = $"{state}";
                    }
                    else
                    {
                        message = formatter.Invoke(state, exception);
                    }

                    _semaphore.Wait();

                    try
                    {
                        InsertTrace(dateTime, logLevel, eventId, message);

                        while (exception != null)
                        {
                            InsertException(dateTime, logLevel, eventId, exception);
                            exception = exception.InnerException;
                        }
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
            });
        }
    }
}