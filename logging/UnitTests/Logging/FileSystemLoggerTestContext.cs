using Accelerate.Testing;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Accelerate.Logging;

/// <summary>
/// Context for tests based on file system logger provider.
/// </summary>
[ExcludeFromCodeCoverage]
public class FileSystemLoggerTestContext : xUnitTestContext
{
    private FileSystemLoggerServiceOptions _loggerServiceOptions;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    public FileSystemLoggerTestContext()
    {
        _loggerServiceOptions = new FileSystemLoggerServiceOptions
        {
            AppName = "FileSystem",
            Critical = true,
            Debug = false,
            Error = true,
            FileName = "filesystem",
            Information = true,
            MaxSize = 1,
            Path = Path.Combine(Environment.CurrentDirectory, "logs"),
            Warning = true
        };
    }

    internal FileSystemLoggerServiceOptions LoggerServiceOptions => _loggerServiceOptions;
}
