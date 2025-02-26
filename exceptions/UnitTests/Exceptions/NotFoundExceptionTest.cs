using Accelerate.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate;

/// <summary>
/// Class to perform unit tests of NotFoundException class.
/// </summary>
[ExcludeFromCodeCoverage]
public class NotFoundExceptionTest : xUnitTest<NotFoundExceptionTestContext>
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Test context.
    /// </param>
    public NotFoundExceptionTest(NotFoundExceptionTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var exception = new NotFoundException();
        Assert.NotNull(exception);
        Assert.Equal("Exception of type 'Accelerate.NotFoundException' was thrown.", exception.Message);
        Assert.Null(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Empty_Message()
    {
        var exception = new NotFoundException(String.Empty);
        Assert.NotNull(exception);
        Assert.Equal(String.Empty, exception.Message);
        Assert.Null(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor using an empty message and a custom inner exception.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Empty_Message_And_Not_Null_Exception()
    {
        var innerException = new Exception();
        var exception = new NotFoundException(String.Empty, innerException);
        Assert.NotNull(exception);
        Assert.Equal(String.Empty, exception.Message);
        Assert.NotNull(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor using an empty message and a null inner exception.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Empty_Message_And_Null_Exception()
    {
        var exception = new NotFoundException(String.Empty, null);
        Assert.NotNull(exception);
        Assert.Equal(String.Empty, exception.Message);
        Assert.Null(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor using a custom message.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Non_Empty_Message()
    {
        var exception = new NotFoundException("message of exception");
        Assert.NotNull(exception);
        Assert.Equal("message of exception", exception.Message);
        Assert.Null(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor using a custom message and a custom inner exception.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Non_Empty_Message_And_Not_Null_Exception()
    {
        var innerException = new Exception();
        var exception = new NotFoundException("message of exception", innerException);
        Assert.NotNull(exception);
        Assert.Equal("message of exception", exception.Message);
        Assert.NotNull(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of constructor using a custom message and a null inner exception.
    /// </summary>
    [Fact]
    public void Ctor_Success_With_Non_Empty_Message_And_Null_Exception()
    {
        var exception = new NotFoundException("message of exception", null);
        Assert.NotNull(exception);
        Assert.Equal("message of exception", exception.Message);
        Assert.Null(exception.InnerException);
    }
    /// <summary>
    /// Method to perform unit test of property Metadata.
    /// </summary>
    [Fact]
    public void Metadata_Success()
    {
        var exception = new NotFoundException
        {
            Metadata = new Dictionary<String, Object> {
                { "key", "value" }
            }
        };

        Assert.Equal("value", exception.Metadata["key"]);
    }
}