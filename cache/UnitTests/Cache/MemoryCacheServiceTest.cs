using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Accelerate.Cache;

/// <summary>
/// Class to perform unit tests of MemoryCacheService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class MemoryCacheServiceTest : xUnitTest<MemoryCacheServiceTestContext>
{
    /// <summary>
    /// Initialize a new instance of MemoryCacheServiceTest class.
    /// </summary>
    /// <param name="context">
    /// Test context.
    /// </param>
    public MemoryCacheServiceTest(MemoryCacheServiceTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);

        Assert.NotNull(cacheService);
    }
    /// <summary>
    /// Method to perform test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_Throwing_Exception_By_Invalid_Options()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cacheService = new MemoryCacheService(null);
        });
    }
    /// <summary>
    /// Method to perform test of add method.
    /// </summary>
    [Fact]
    public void Add_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");
        var value = cacheService.Get<String>(key);

        Assert.Equal("memory cache service", value);
    }
    /// <summary>
    /// Method to perform test of add method raising an exception of type ConflictException.
    /// </summary>
    [Fact]
    public void Add_Throwing_Exception_By_Duplicated_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");

        Assert.Throws<ConflictException>(() =>
        {
            cacheService.Add(key, "memory cache service");
        });
    }
    /// <summary>
    /// Method to perform test of add method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Add_Throwing_Exception_By_Invalid_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);

        Assert.Throws<ArgumentException>(() =>
        {
            cacheService.Add(null, "memory cache service");
        });
    }
    /// <summary>
    /// Method to perform test of add method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Add_Throwing_Exception_By_Invalid_Value()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";

        Assert.Throws<ArgumentException>(() =>
        {
            cacheService.Add<String>(key, null);
        });
    }
    /// <summary>
    /// Method to perform test of add method raising an exception of type OverflowException.
    /// </summary>
    [Fact]
    public void Add_Throwing_Exception_By_Size()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);

        Assert.Throws<OverflowException>(() =>
        {
            for (var i = 0; i <= Context.MaxCacheSize; i++)
            {
                var key = $"{Guid.NewGuid()}";
                cacheService.Add(key, "memory cache service");
            }
        });
    }
    /// <summary>
    /// Method to perform test of clear method.
    /// </summary>
    [Fact]
    public void Clear_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");
        cacheService.Clear();

        Assert.Throws<NotFoundException>(() =>
        {
            var value = cacheService.Get<String>(key);
        });
    }
    /// <summary>
    /// Method to perform test of contains method.
    /// </summary>
    [Fact]
    public void Contains_Inexistent_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        var contains = cacheService.Contains(key);

        Assert.False(contains);
    }
    /// <summary>
    /// Method to perform test of contains method.
    /// </summary>
    [Fact]
    public void Contains_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");
        var contains = cacheService.Contains(key);

        Assert.True(contains);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        cacheService.Dispose();

        Assert.NotNull(cacheService);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        cacheService.Dispose();

        Assert.Throws<ObjectDisposedException>(() =>
        {
            cacheService.Dispose();
        });
    }
    /// <summary>
    /// Method to perform test of get method.
    /// </summary>
    [Fact]
    public void Get_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");
        var value = cacheService.Get<String>(key);

        Assert.NotNull(value);
    }
    /// <summary>
    /// Method to perform test of get method, retrieving an expired value.
    /// </summary>
    [Fact]
    public async Task Get_Throwing_Exception_By_Expired_Value()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service", DateTime.UtcNow.AddSeconds(1));

        await Task.Delay(1250);

        Assert.Throws<NotFoundException>(() =>
        {
            var value = cacheService.Get<String>(key);
        });
    }
    /// <summary>
    /// Method to perform test of get method using an inexistent key.
    /// </summary>
    [Fact]
    public void Get_Throwing_Exception_By_Inexistent_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";

        Assert.Throws<NotFoundException>(() =>
        {
            var value = cacheService.Get<String>(key);
        });
    }
    /// <summary>
    /// Method to perform test of get method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Get_Throwing_Exception_By_Invalid_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);

        Assert.Throws<ArgumentException>(() =>
        {
            var value = cacheService.Get<String>(null);
        });
    }
    /// <summary>
    /// Method to perform test of remove method.
    /// </summary>
    [Fact]
    public void Remove_Success()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";
        cacheService.Add(key, "memory cache service");
        cacheService.Remove(key);

        Assert.Throws<NotFoundException>(() =>
        {
            var value = cacheService.Get<String>(key);
        });
    }
    /// <summary>
    /// Method to perform test of remove method raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Remove_Throwing_Exception_By_Invalid_Key()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);

        Assert.Throws<ArgumentException>(() =>
        {
            cacheService.Remove(null);
        });
    }
    /// <summary>
    /// Method to perform test of remove method raising an exception of type NotFoundException.
    /// </summary>
    [Fact]
    public void Remove_Throwing_Exception_By_Key_Not_Found()
    {
        var options = Options.Create(new MemoryCacheServiceOptions
        {
            DefaultExpirationTime = Context.DefaultExpirationTime,
            MaxCacheSize = Context.MaxCacheSize
        });
        var cacheService = new MemoryCacheService(options);
        var key = $"{Guid.NewGuid()}";

        Assert.Throws<NotFoundException>(() =>
        {
            cacheService.Remove(key);
        });
    }
}