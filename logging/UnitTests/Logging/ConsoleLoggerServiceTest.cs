using Accelerate.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Accelerate.Logging;

/// <summary>
/// Class to perform unit tests of ConsoleLoggerService class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ConsoleLoggerServiceTest : xUnitTest<ConsoleLoggerTestContext>
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    /// <param name="context">
    /// Test execution context.
    /// </param>
    public ConsoleLoggerServiceTest(ConsoleLoggerTestContext context) : base(context)
    {
    }

    /// <summary>
    /// Method to perform test of BeginScope method.
    /// </summary>
    [Fact]
    public void BeginScope_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerService = new ConsoleLoggerService(options);
        var scopeObject = loggerService.BeginScope("lorem ipsum");

        Assert.NotNull(scopeObject);
    }
    /// <summary>
    /// Method to perform test of BeginScope method.
    /// </summary>
    [Fact]
    public void BeginScope_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerService = new ConsoleLoggerService(options);
            var scopeObject = loggerService.BeginScope<String>(null);
        });
    }
    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerService = new ConsoleLoggerService(options);

        Assert.NotNull(loggerService);
    }
    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_Throwing_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var loggerService = new ConsoleLoggerService(null);
        });
    }
    /// <summary>
    /// Method to perform unit test of constructor.
    /// </summary>
    [Fact]
    public void Ctor_With_Category()
    {
        var options = Options.Create(Context.LoggerServiceOptions);
        var loggerService = new ConsoleLoggerService(options, "Default");

        Assert.NotNull(loggerService);
    }
    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_With_Category_Throwing_Exception_By_Category()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerService = new ConsoleLoggerService(options, null);
        });
    }
    /// <summary>
    /// Method to perform unit test of constructor raising an exception of type ArgumentException.
    /// </summary>
    [Fact]
    public void Ctor_With_Category_Throwing_Exception_By_Options()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var loggerService = new ConsoleLoggerService(null, "Default");
        });
    }
    /// <summary>
    /// Method to perform test of dispose method.
    /// </summary>
    [Fact]
    public void Dispose_Success()
    {
        var options = Options.Create(Context.LoggerServiceOptions);

        using var loggerService = new ConsoleLoggerService(options);

        Assert.NotNull(loggerService);
    }
    /// <summary>
    /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
    /// </summary>
    [Fact]
    public void Dispose_Throwing_Exception()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            var options = Options.Create(Context.LoggerServiceOptions);

            using var loggerService = new ConsoleLoggerService(options);

            loggerService.Dispose();
        });
    }
    /// <summary>
    /// Method to perform test of log method.
    /// </summary>
    [Fact]
    public async Task Log_SuccessAsync()
    {
        try
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerService = new ConsoleLoggerService(options, "Default");
            var innerException = new Exception("inner exception");
            var mainException = new Exception("main exception", innerException);

            loggerService.Log(LogLevel.Debug,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              (String state, Exception exception) => { return state; });

            loggerService.Log(LogLevel.Critical,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              (String state, Exception exception) => { return state; });

            loggerService.Log(LogLevel.Error,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              (String state, Exception exception) => { return state; });

            loggerService.Log(LogLevel.Information,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              (String state, Exception exception) => { return state; });

            loggerService.Log(LogLevel.Warning,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              (String state, Exception exception) => { return state; });

            await Task.Delay(3000);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Method to perform test of log method.
    /// </summary>
    [Fact]
    public void Log_With_Null_Exception()
    {
        try
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerService = new ConsoleLoggerService(options, "Default");

            loggerService.Log(LogLevel.Information,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              null,
                              (String state, Exception exception) => { return state; });
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Method to perform test of log method.
    /// </summary>
    [Fact]
    public void Log_With_Null_Formatter()
    {
        try
        {
            var options = Options.Create(Context.LoggerServiceOptions);
            var loggerService = new ConsoleLoggerService(options, "Default");
            var innerException = new Exception("inner exception");
            var mainException = new Exception("main exception", innerException);

            loggerService.Log(LogLevel.Information,
                              new EventId(0, "Default"),
                              "lorem ipsum",
                              mainException,
                              null);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
