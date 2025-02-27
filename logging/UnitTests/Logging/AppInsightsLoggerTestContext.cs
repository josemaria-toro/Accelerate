using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Logging;

/// <summary>
/// Context for tests based on azure application insights logger provider.
/// </summary>
[ExcludeFromCodeCoverage]
public class AppInsightsLoggerTestContext : xUnitTestContext
{
    private AppInsightsLoggerServiceOptions _loggerServiceOptions;

    /// <summary>
    /// Initialize a new instance of class.
    /// </summary>
    public AppInsightsLoggerTestContext()
    {
        _loggerServiceOptions = new AppInsightsLoggerServiceOptions
        {
            AppName = "AppInsights",
            ConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://dc.applicationinsights.azure.com;LiveEndpoint=https://live.applicationinsights.azure.com;ProfilerEndpoint=https://profiler.monitor.azure.com;SnapshotEndpoint=https://snapshot.monitor.azure.com;",
            Critical = true,
            Debug = false,
            Error = true,
            Information = true,
            Warning = true
        };
    }

    internal AppInsightsLoggerServiceOptions LoggerServiceOptions => _loggerServiceOptions;
}
