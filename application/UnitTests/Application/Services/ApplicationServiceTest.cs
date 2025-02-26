using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Application.Services;

/// <summary>
/// Class to perform unit tests of ApplicationService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationServiceTest : xUnitTest<ApplicationServiceTestContext>
{
    /// <summary>
    /// Initializes a new instance of the ApplicationServiceTest class.
    /// </summary>
    /// <param name="context">
    /// Test context.
    /// </param>
    public ApplicationServiceTest(ApplicationServiceTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var applicationService = new ApplicationServiceClass();

        Assert.NotNull(applicationService);
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var applicationService = new ApplicationServiceClass();

        Assert.NotNull(applicationService);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var applicationService = new ApplicationServiceClass();

            applicationService.Dispose();
        });
    }
}