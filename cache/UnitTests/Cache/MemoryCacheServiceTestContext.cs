using Accelerate.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Cache;

/// <summary>
/// Context for tests of MemoryCacheService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class MemoryCacheServiceTestContext : xUnitTestContext
{
    /// <summary>
    /// Initialize a new instance of MemoryCacheServiceTestContext class.
    /// </summary>
    public MemoryCacheServiceTestContext() : base()
    {
        DefaultExpirationTime = Configuration.GetValue<Int32>("memory:defautExpirationTime");
        MaxCacheSize = Configuration.GetValue<Int32>("memory:maxCacheSize");
    }

    /// <summary>
    /// Default expiration time, in minutes.
    /// </summary>
    public Int32 DefaultExpirationTime { get; set; }
    /// <summary>
    /// Maximum number of objects allowed in the service.
    /// </summary>
    public Int32 MaxCacheSize { get; set; }
}