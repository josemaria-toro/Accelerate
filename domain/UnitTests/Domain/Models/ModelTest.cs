using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Domain.Models;

/// <summary>
/// Class to perform unit tests of Model class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ModelTest : xUnitTest<ModelTestContext>
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Test execution context.
    /// </param>
    public ModelTest(ModelTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var model = new ModelClass();

        Assert.NotNull(model);
    }
    /// <summary>
    /// Method to perform unit test of clone method.
    /// </summary>
    [Fact]
    public void Clone_Success()
    {
        var model = new ModelClass();
        var modelCloned = model.Clone();

        Assert.Equivalent(model, modelCloned);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var model = new ModelClass();

        Assert.NotNull(model);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var model = new ModelClass();

            model.Dispose();
        });
    }
}