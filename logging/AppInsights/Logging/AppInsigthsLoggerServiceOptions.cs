using System;

namespace Accelerate.Logging
{
    /// <summary>
    /// Configuration options for azure application insights logger service.
    /// </summary>
    public sealed class AppInsightsLoggerServiceOptions : LoggerServiceOptions
    {
        /// <summary>
        /// Connection string to azure application insights.
        /// </summary>
        public String ConnectionString { get; set; }
    }
}