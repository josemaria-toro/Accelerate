using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Data;

using OptionsCreator = Microsoft.Extensions.Options.Options;

namespace Accelerate.Logging
{
    /// <summary>
    /// Represent a type that can create instances of type ILogger based on database.
    /// </summary>
    [ProviderAlias("Database")]
    public sealed class DatabaseLoggerProvider : LoggerProvider<DatabaseLoggerServiceOptions>
    {
        private readonly IDbConnection _connection;
        private Boolean _disposed;
        private ConcurrentDictionary<String, ILogger> _loggerServices;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger provider.
        /// </param>
        /// <param name="connection">
        /// Database connection.
        /// </param>
        public DatabaseLoggerProvider(IOptions<DatabaseLoggerServiceOptions> options, IDbConnection connection) : base(options)
        {
            _connection = connection ?? throw new ArgumentException("Database connection cannot be null", nameof(connection));
            _loggerServices = new ConcurrentDictionary<String, ILogger>();
        }

        /// <inheritdoc />
        public override ILogger CreateLogger(String categoryName)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("The category cannot be null or empty", nameof(categoryName));
            }

            var options = OptionsCreator.Create(Options);

            return _loggerServices.GetOrAdd(categoryName, _ => new DatabaseLoggerService(options, _connection, categoryName));
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
                _loggerServices = null;
            }

            _disposed = true;
        }
    }
}