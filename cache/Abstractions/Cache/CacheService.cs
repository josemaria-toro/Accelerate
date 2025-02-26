using Microsoft.Extensions.Options;
using System;

namespace Accelerate.Cache
{
    /// <summary>
    /// Base class for services to manage cache objects.
    /// </summary>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
    public abstract class CacheService<TOptions> : ICacheService where TOptions : CacheServiceOptions
    {
        private Boolean _disposed;
        private TOptions _options;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for memory cache service.
        /// </param>
        protected CacheService(IOptions<TOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentException("Options for cache service cannot be null", nameof(options));
        }

        /// <summary>
        /// Configuration options.
        /// </summary>
        protected TOptions Options => _options;

        /// <inheritdoc />
        public abstract void Add<TValue>(String key, TValue value);
        /// <inheritdoc />
        public abstract void Add<TValue>(String key, TValue value, DateTime expiredAt);
        /// <inheritdoc />
        public abstract void Clear();
        /// <inheritdoc />
        public abstract Boolean Contains(String key);
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
        /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
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
        /// <inheritdoc />
        public abstract TValue Get<TValue>(String key);
        /// <inheritdoc />
        public abstract void Remove(String key);
    }
}