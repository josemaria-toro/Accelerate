using System;

namespace Accelerate.Cache
{
    /// <summary>
    /// Configuration options for cache services.
    /// </summary>
    public abstract class CacheServiceOptions
    {
        /// <summary>
        /// Default expiration time, in minutes.
        /// </summary>
        public Int32 DefaultExpirationTime { get; set; }
        /// <summary>
        /// Maximum number of values allowed in the service.
        /// </summary>
        public Int32 MaxCacheSize { get; set; }
    }
}