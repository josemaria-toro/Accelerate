using System;
using System.Collections.Generic;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Event information.
    /// </summary>
    public sealed class Event
    {
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Metadata associated to this event.
        /// </summary>
        public IDictionary<String, Object> Metadata { get; set; }
        /// <summary>
        /// Name of event.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Date and time when event happened.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}