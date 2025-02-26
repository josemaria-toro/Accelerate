using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Data.Repositories;

/// <summary>
/// Class to perform unit tests of Repository class.
/// </summary>
[ExcludeFromCodeCoverage]
public class RepositoryTest : xUnitTest<RepositoryTestContext>
{
    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Context for tests.
    /// </param>
    public RepositoryTest(RepositoryTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_Throwing_Exception_By_Invalid_Options()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var repository = new RepositoryClass(null);
        });
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        using var repository = new RepositoryClass(Options.Create(new RepositoryClassOptions
        {
            ConnectionString = "",
            Timeout = 30
        }));

        Assert.NotNull(repository);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            using var repository = new RepositoryClass(Options.Create(new RepositoryClassOptions
            {
                ConnectionString = "",
                Timeout = 30
            }));

            repository.Dispose();
        });
    }
    /// <summary>
    /// Method to perform test of options property.
    /// </summary>
    [Fact]
    public void Read_Options()
    {
        try
        {
            using var repository = new RepositoryClass(Options.Create(new RepositoryClassOptions
            {
                ConnectionString = "",
                Timeout = 30
            }));

            var connectionString = repository.Options.ConnectionString;
            var timeout = repository.Options.Timeout;
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
