using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Request information.
    /// </summary>
    public sealed class HttpClientRequest
    {
        /// <summary>
        /// Data to send.
        /// </summary>
        public String Body { get; set; }
        /// <summary>
        /// List of cookies.
        /// </summary>
        public IEnumerable<Cookie> Cookies { get; set; }
        /// <summary>
        /// Content type of the request body.
        /// </summary>
        public String ContentType { get; set; }
        /// <summary>
        /// List of headers.
        /// </summary>
        public IDictionary<String, String> Headers { get; set; }
        /// <summary>
        /// Method to use.
        /// </summary>
        public HttpMethod Method { get; set; }
        /// <summary>
        /// Url parameters.
        /// </summary>
        public IDictionary<String, Object> Parameters { get; set; }
        /// <summary>
        /// Url path.
        /// </summary>
        public String Path { get; set; }
    }
}