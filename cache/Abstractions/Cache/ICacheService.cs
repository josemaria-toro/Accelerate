using System;

namespace Accelerate.Cache
{
    /// <summary>
    /// Service to manage cache objects.
    /// </summary>
    public interface ICacheService : IDisposable
    {
        /// <summary>
        /// Add a value to cache.
        /// </summary>
        /// <typeparam name="TValue">
        /// Type of value to add.
        /// </typeparam>
        /// <param name="key">
        /// Unique key to identify the value in the cache.
        /// </param>
        /// <param name="value">
        /// Value to add to cache.
        /// </param>
        void Add<TValue>(String key, TValue value);
        /// <summary>
        /// Add a value to cache.
        /// </summary>
        /// <typeparam name="TValue">
        /// Type of value to add.
        /// </typeparam>
        /// <param name="key">
        /// Unique key to identify the value in the cache.
        /// </param>
        /// <param name="value">
        /// Value to add to cache.
        /// </param>
        /// <param name="expiredAt">
        /// Cache value expiration date.
        /// </param>
        void Add<TValue>(String key, TValue value, DateTime expiredAt);
        /// <summary>
        /// Remove all values in the cache.
        /// </summary>
        void Clear();
        /// <summary>
        /// Check if a key is in the cache.
        /// </summary>
        /// <param name="key">
        /// Key to check if it's contained.
        /// </param>
        Boolean Contains(String key);
        /// <summary>
        /// Get the value with the specified key.
        /// </summary>
        /// <typeparam name="TValue">
        /// Type of value to get.
        /// </typeparam>
        /// <param name="key">
        /// Unique key to identify the value in the cache.
        /// </param>
        TValue Get<TValue>(String key);
        /// <summary>
        /// Remove the value with the specified key.
        /// </summary>
        /// <param name="key">
        /// Unique key to identify the value in the cache.
        /// </param>
        void Remove(String key);
    }
}