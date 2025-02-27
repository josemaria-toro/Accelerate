using System;
using System.Collections.Generic;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Page information.
    /// </summary>
    public sealed class PageView
    {
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Time the user was on the page.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Metadata associated to this page.
        /// </summary>
        public IDictionary<String, Object> Metadata { get; set; }
        /// <summary>
        /// Name of page.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Date and time when page was viewed.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Uri of page.
        /// </summary>
        public Uri Uri { get; set; }
    }
}