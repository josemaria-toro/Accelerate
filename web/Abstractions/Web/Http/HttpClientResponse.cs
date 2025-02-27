using System;
using System.Collections.Generic;
using System.Net;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Response data received from a remote endpoint.
    /// </summary>
    public sealed class HttpClientResponse
    {
        /// <summary>
        /// Data received from the remote endpoint.
        /// </summary>
        public String Body { get; set; }
        /// <summary>
        /// List of headers receivedfrom the remote endpoint.
        /// </summary>
        public IDictionary<String, String> Headers { get; set; }
        /// <summary>
        /// Status message of the response.
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// Status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}