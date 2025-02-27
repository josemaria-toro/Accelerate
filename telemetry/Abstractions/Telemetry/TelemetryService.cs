using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Base class for telemetry services.
    /// </summary>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
    public abstract class TelemetryService<TOptions> : ITelemetryService where TOptions : TelemetryServiceOptions
    {
        private Boolean _disposed;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options.
        /// </param>
        protected TelemetryService(IOptions<TOptions> options)
        {
            Options = options?.Value ?? throw new ArgumentException("Configuration options cannot be null", nameof(options));
        }

        /// <summary>
        /// Configuration options.
        /// </summary>
        protected TOptions Options { get; private set; }

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

            _disposed = true;
        }
        /// <inheritdoc />
        public abstract void Track(Availability availability);
        /// <inheritdoc />
        public abstract void Track(Dependency dependency);
        /// <inheritdoc />
        public abstract void Track(Event @event);
        /// <inheritdoc />
        public abstract void Track(Metric metric);
        /// <inheritdoc />
        public abstract void Track(PageView pageView);
        /// <inheritdoc />
        public abstract void Track(Request request);
    }

}