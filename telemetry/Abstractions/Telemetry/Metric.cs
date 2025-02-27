using System;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Metric information.
    /// </summary>
    public sealed class Metric
    {
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Dimension of the metric.
        /// </summary>
        public String Dimension { get; set; }
        /// <summary>
        /// Name of metric.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Value of metric.
        /// </summary>
        public Double Value { get; set; }
    }
}