using System;

namespace Accelerate.Cache
{
    /// <summary>
    /// Class for manage the objects stored in the cache service.
    /// </summary>
    internal class MemoryCacheServiceObject
    {
        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Expiration date.
        /// </summary>
        public DateTime ExpiredAt { get; set; }
        /// <summary>
        /// Unique key.
        /// </summary>
        public String Key { get; set; }
        /// <summary>
        /// Stored value.
        /// </summary>
        public Object Value { get; set; }
    }
}