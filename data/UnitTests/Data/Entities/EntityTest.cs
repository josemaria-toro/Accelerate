using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Data.Entities;

/// <summary>
/// Class to perform unit tests of Entity class.
/// </summary>
[ExcludeFromCodeCoverage]
public class EntityTest : xUnitTest<EntityTestContext>
{
    /// <summary>
    /// Initializes a new instance of the EntityTest class.
    /// </summary>
    /// <param name="context">
    /// Test execution context.
    /// </param>
    public EntityTest(EntityTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor()
    {
        var entity = new EntityClass();

        Assert.NotNull(entity);
    }
    /// <summary>
    /// Method to perform unit test of clone method.
    /// </summary>
    [Fact]
    public void Clone()
    {
        var entity = new EntityClass();
        var entityCloned = entity.Clone();

        Assert.Equivalent(entity, entityCloned);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var entity = new EntityClass();

        Assert.NotNull(entity);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var entity = new EntityClass();

            entity.Dispose();
        });
    }
}