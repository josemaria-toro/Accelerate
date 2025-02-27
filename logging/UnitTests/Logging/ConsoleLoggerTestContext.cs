using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Logging;

/// <summary>
/// Context for tests based on console logger provider.
/// </summary>
[ExcludeFromCodeCoverage]
public class ConsoleLoggerTestContext : xUnitTestContext
{
    private ConsoleLoggerServiceOptions _loggerServiceOptions;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    public ConsoleLoggerTestContext()
    {
        _loggerServiceOptions = new ConsoleLoggerServiceOptions
        {
            AppName = "Console",
            Critical = true,
            CriticalColor = ConsoleColor.DarkRed,
            Debug = false,
            DebugColor = ConsoleColor.Blue,
            Error = true,
            ErrorColor = ConsoleColor.Red,
            Information = true,
            InformationColor = ConsoleColor.White,
            Warning = true,
            WarningColor = ConsoleColor.Yellow,
        };
    }

    internal ConsoleLoggerServiceOptions LoggerServiceOptions => _loggerServiceOptions;
}
