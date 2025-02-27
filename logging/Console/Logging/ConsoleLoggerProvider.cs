using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

using OptionsCreator = Microsoft.Extensions.Options.Options;

namespace Accelerate.Logging
{
    /// <summary>
    /// Represent a type that can create instances of type ILogger based on system console.
    /// </summary>
    [ProviderAlias("Console")]
    public sealed class ConsoleLoggerProvider : LoggerProvider<ConsoleLoggerServiceOptions>
    {
        private Boolean _disposed;
        private ConcurrentDictionary<String, ILogger> _loggerServices;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for logger provider.
        /// </param>
        public ConsoleLoggerProvider(IOptions<ConsoleLoggerServiceOptions> options) : base(options)
        {
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

            return _loggerServices.GetOrAdd(categoryName, _ => new ConsoleLoggerService(options, categoryName));
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