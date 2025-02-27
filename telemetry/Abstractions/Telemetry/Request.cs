using System;
using System.Collections.Generic;
using System.Net;

namespace Accelerate.Telemetry
{
    /// <summary>
    /// Request information.
    /// </summary>
    public sealed class Request
    {
        /// <summary>
        /// IP address or domain name of the client.
        /// </summary>
        public String Client { get; set; }
        /// <summary>
        /// Correlation id.
        /// </summary>
        public String CorrelationId { get; set; }
        /// <summary>
        /// Duration of the request.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Metadata associated to this request.
        /// </summary>
        public IDictionary<String, Object> Metadata { get; set; }
        /// <summary>
        /// Name of request.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Code of the response.
        /// </summary>
        public HttpStatusCode ResponseCode { get; set; }
        /// <summary>
        /// Flag that indicates if request response was successfully or not.
        /// </summary>
        public Boolean Success { get; set; }
        /// <summary>
        /// Date and time when request was done.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Uri of the request.
        /// </summary>
        public Uri Uri { get; set; }
    }

}