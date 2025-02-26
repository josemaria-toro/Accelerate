using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Accelerate.Cache
{
    /// <summary>
    /// Infrastructure service to manage cache objects based on physical memory.
    /// </summary>
    public sealed class MemoryCacheService : CacheService<MemoryCacheServiceOptions>
    {
        private ConcurrentDictionary<String, MemoryCacheServiceObject> _dictionary;
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for memory cache service.
        /// </param>
        public MemoryCacheService(IOptions<MemoryCacheServiceOptions> options) : base(options)
        {
            _dictionary = new ConcurrentDictionary<String, MemoryCacheServiceObject>();
        }

        /// <inheritdoc />
        public override void Add<TValue>(String key, TValue value)
        {
            Add(key, value, DateTime.UtcNow.AddMinutes(Options.DefaultExpirationTime));
        }
        /// <inheritdoc />
        public override void Add<TValue>(String key, TValue value, DateTime expiredAt)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be added to the cache service because the provided key is invalid", nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentException("Value cannot be added to the cache service because the provided value is invalid", nameof(value));
            }

            Purge();

            if (_dictionary.Count >= Options.MaxCacheSize)
            {
                throw new OverflowException("Value cannot be added because the maximum size is surpassed");
            }

            if (_dictionary.ContainsKey(key))
            {
                throw new ConflictException("Value cannot be added because already exists an Object with the same key");
            }

            if (DateTime.UtcNow < expiredAt.ToUniversalTime())
            {
                _dictionary.TryAdd(key, new MemoryCacheServiceObject
                {
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = expiredAt.ToUniversalTime(),
                    Key = key,
                    Value = value
                });
            }
        }
        /// <inheritdoc />
        public override void Clear()
        {
            _dictionary.Clear();
        }
        /// <inheritdoc />
        public override Boolean Contains(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be checked because the provided key is invalid", nameof(key));
            }

            Purge();

            return _dictionary.ContainsKey(key);
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
                _dictionary = null;
            }

            _disposed = true;
        }
        /// <inheritdoc />
        public override TValue Get<TValue>(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be retrieved from the cache service because the provided key is invalid", nameof(key));
            }

            Purge();

            if (!_dictionary.TryGetValue(key, out var memoryCacheServiceObject))
            {
                throw new NotFoundException("Value cannot be retrieved from the cache service because the key was not found");
            }

            return (TValue)memoryCacheServiceObject.Value;
        }
        /// <summary>
        /// Remove all expired values in the cache.
        /// </summary>
        private void Purge()
        {
            var expiredObjects = _dictionary.Values.Where(x => x.ExpiredAt < DateTime.UtcNow);

            foreach (var expiredObject in expiredObjects)
            {
                _dictionary.TryRemove(expiredObject.Key, out var _);
            }
        }
        /// <inheritdoc />
        public override void Remove(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be removed from the cache service because the provided key is invalid", nameof(key));
            }

            Purge();

            if (!_dictionary.ContainsKey(key))
            {
                throw new NotFoundException("Value cannot be removed from the cache service because the key was not found");
            }

            _dictionary.TryRemove(key, out var _);
        }
    }
}