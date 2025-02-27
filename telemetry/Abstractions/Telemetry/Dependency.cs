using System;
using System.Collections.Generic;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Application or service dependency information.
    /// </summary>
    public sealed class Dependency
    {
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Data sended to target (sql command, http request body, etc.).
        /// </summary>
        public String Data { get; set; }
        /// <summary>
        /// Metadata associated to this dependency.
        /// </summary>
        public IDictionary<String, Object> Metadata { get; set; }
        /// <summary>
        /// Duration of dependency call.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Name of dependency.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Results of dependency call (sql data, http response body, etc.).
        /// </summary>
        public String Results { get; set; }
        /// <summary>
        /// Flag that indicates if dependency result was successfully or not.
        /// </summary>
        public Boolean Success { get; set; }
        /// <summary>
        /// Target of dependency call (database server name, http request url, machine name, etc.).
        /// </summary>
        public String Target { get; set; }
        /// <summary>
        /// Date and time when dependency was called.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Type of dependency.
        /// </summary>
        public String Type { get; set; }
    }
}