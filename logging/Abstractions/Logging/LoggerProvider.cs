using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Represent a type that can create instances of type ILogger.
    /// </summary>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
    public abstract class LoggerProvider<TOptions> : ILoggerProvider where TOptions : LoggerServiceOptions
    {
        private Boolean _disposed;
        private TOptions _options;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger provider.
        /// </param>
        protected LoggerProvider(IOptions<TOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentException("Configuration options cannot be null", nameof(options));
        }

        /// <summary>
        /// Configuration options.
        /// </summary>
        protected TOptions Options => _options;

        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="categoryName">
        /// The category name for messages produced by the logger.
        /// </param>
        public abstract ILogger CreateLogger(String categoryName);
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
                _options = null;
            }

            _disposed = true;
        }
    }
}