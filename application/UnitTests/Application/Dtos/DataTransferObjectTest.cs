using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Application.Dtos;

/// <summary>
/// Class to perform unit tests of DataTransferObject class.
/// </summary>
[ExcludeFromCodeCoverage]
public class DataTransferObjectTest : xUnitTest<DataTransferObjectTestContext>
{
    /// <summary>
    /// Initializes a new instance of DataTransferObjectTest class.
    /// </summary>
    /// <param name="context">
    /// Test context.
    /// </param>
    public DataTransferObjectTest(DataTransferObjectTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var dataTransferObject = new DataTransferObjectClass();

        Assert.NotNull(dataTransferObject);
    }
    /// <summary>
    /// Method to perform unit test of clone method.
    /// </summary>
    [Fact]
    public void Clone_Success()
    {
        var dataTransferObject = new DataTransferObjectClass();
        var dataTransferObjectCloned = dataTransferObject.Clone();

        Assert.Equivalent(dataTransferObject, dataTransferObjectCloned);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var dataTransferObject = new DataTransferObjectClass();

        Assert.NotNull(dataTransferObject);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var dataTransferObject = new DataTransferObjectClass();

            dataTransferObject.Dispose();
        });
    }
}