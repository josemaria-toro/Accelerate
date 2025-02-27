using System;
using System.Security.Cryptography.X509Certificates;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Configuration options for http clients.
    /// </summary>
    public sealed class HttpClientOptions
    {
        /// <summary>
        /// Base url of the remote endpoint.
        /// </summary>
        public String BaseUrl { get; set; }
        /// <summary>
        /// Client certificate.
        /// </summary>
        public X509Certificate ClientCertificate { get; set; }
        /// <summary>
        /// Time to wait a response, before service agent throws an error.
        /// </summary>
        public Int32 Timeout { get; set; }
    }
}