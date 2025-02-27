using System;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Base class for telemetry services configuration options.
    /// </summary>
    public abstract class TelemetryServiceOptions
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public String AppName { get; set; }
    }
}