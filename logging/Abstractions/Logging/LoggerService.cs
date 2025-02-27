using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Base class for logger services.
    /// </summary>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
    public abstract class LoggerService<TOptions> : ILogger, IDisposable where TOptions : LoggerServiceOptions
    {
        private String _categoryName;
        private Boolean _disposed;
        private TOptions _options;
        private IExternalScopeProvider _scopeProvider;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options.
        /// </param>
        protected LoggerService(IOptions<TOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentException("Configuration options cannot be null", nameof(options));
            _scopeProvider = new LoggerExternalScopeProvider();

            if (String.IsNullOrEmpty(Options.AppName))
            {
                Options.AppName = Environment.MachineName;
            }
        }
        /// <summary>
        /// Initialize a new instance of <see cref="LoggerService{TOptions}" /> class.
        /// </summary>
        /// <param name="options">
        /// Configuration options.
        /// </param>
        /// <param name="categoryName">
        /// The category name for messages produced by the logger.
        /// </param>
        protected LoggerService(IOptions<TOptions> options, String categoryName)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("The category cannot be null or empty", nameof(categoryName));
            }

            _categoryName = categoryName;
            _options = options?.Value ?? throw new ArgumentException("Configuration options for logger provider", nameof(options));
            _scopeProvider = new LoggerExternalScopeProvider();
        }

        /// <summary>
        /// The category name for messages produced by the logger.
        /// </summary>
        protected String CategoryName => _categoryName;
        /// <summary>
        /// Configuration options.
        /// </summary>
        protected TOptions Options => _options;

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">
        /// The type of the state to begin scope for.
        /// </typeparam>
        /// <param name="state">
        /// The identifier for the scope.
        /// </param>
        public virtual IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
            {
                throw new ArgumentException("Scope state cannot be null", nameof(state));
            }

            return _scopeProvider.Push(state);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (disposing)
            {
                _categoryName = null;
                _options = null;
                _scopeProvider = null;
            }

            _disposed = true;
        }
        /// <summary>
        /// Checks if the given logLevel is enabled.
        /// </summary>
        /// <param name="logLevel">
        /// Log level to be checked.
        /// </param>
        public virtual Boolean IsEnabled(LogLevel logLevel)
        {
            return (logLevel == LogLevel.Critical && _options.Critical) ||
                   (logLevel == LogLevel.Debug && _options.Debug) ||
                   (logLevel == LogLevel.Error && _options.Error) ||
                   (logLevel == LogLevel.Information && _options.Information) ||
                   (logLevel == LogLevel.Warning && _options.Warning);
        }
        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">
        /// The type of the object to be written.
        /// </typeparam>
        /// <param name="logLevel">
        /// Entry will be written on this level.
        /// </param>
        /// <param name="eventId">
        /// Id of the event.
        /// </param>
        /// <param name="state">
        /// The entry to be written.
        /// </param>
        /// <param name="exception">
        /// The exception related to this entry.
        /// </param>
        /// <param name="formatter">
        /// Function to create a string message of the state and exception.
        /// </param>
        public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter);
    }
}