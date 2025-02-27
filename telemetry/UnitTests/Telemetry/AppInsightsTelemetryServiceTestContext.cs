using Accelerate.Testing;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Telemetry;

/// <summary>
/// Context for tests based on azure application insights telemetry provider.
/// </summary>
[ExcludeFromCodeCoverage]
public class AppInsightsTelemetryServiceTestContext : xUnitTestContext
{
    private AppInsightsTelemetryServiceOptions _telemetryServiceOptions;

    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public AppInsightsTelemetryServiceTestContext()
    {
        _telemetryServiceOptions = new AppInsightsTelemetryServiceOptions
        {
            AppName = "AppInsights",
            ConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://dc.applicationinsights.azure.com;LiveEndpoint=https://live.applicationinsights.azure.com;ProfilerEndpoint=https://profiler.monitor.azure.com;SnapshotEndpoint=https://snapshot.monitor.azure.com;"
        };
    }

    internal AppInsightsTelemetryServiceOptions TelemetryServiceOptions => _telemetryServiceOptions;
}
