using System;
using System.Collections.Generic;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Availability test information.
    /// </summary>
    public sealed class Availability
    {
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Duration of the availability test.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Availability test result message.
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// Metadata associated to the availability test.
        /// </summary>
        public IDictionary<String, Object> Metadata { get; set; }
        /// <summary>
        /// Name of service tested.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Flag that indicates if the availability test result was successfully or not.
        /// </summary>
        public Boolean Success { get; set; }
        /// <summary>
        /// Date and time when availability test was executed.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}