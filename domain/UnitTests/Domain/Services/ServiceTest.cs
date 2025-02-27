using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Domain.Services;

/// <summary>
/// Class to perform unit tests of DomainService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ServiceTest : xUnitTest<ServiceTestContext>
{
    /// <summary>
    /// Initializes a new instance of the ServiceTest class.
    /// </summary>
    /// <param name="context">
    /// Test context.
    /// </param>
    public ServiceTest(ServiceTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var service = new ServiceClass();

        Assert.NotNull(service);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var service = new ServiceClass();

        Assert.NotNull(service);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var service = new ServiceClass();

            service.Dispose();
        });
    }
}