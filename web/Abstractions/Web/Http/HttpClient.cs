using Accelerate.Web.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using HttpClientNet = System.Net.Http.HttpClient;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Base class for http clients.
    /// </summary>
    public abstract class HttpClient : IHttpClient
    {
        private Boolean _disposed;
        private HttpClientNet _httpClient;
        private HttpClientOptions _options;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for http client.
        /// </param>
        protected HttpClient(IOptions<HttpClientOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentException("Options for http client cannot be null", nameof(options));

            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.None,
                CheckCertificateRevocationList = false,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                CookieContainer = new CookieContainer(),
                MaxAutomaticRedirections = 50,
                MaxConnectionsPerServer = Int32.MaxValue,
                MaxResponseHeadersLength = 64,
                PreAuthenticate = false,
                SslProtocols = SslProtocols.None,
                UseCookies = true,
                UseDefaultCredentials = false,
                UseProxy = true
            };

            if (_options.ClientCertificate != null)
            {
                httpClientHandler.ClientCertificates.Add(_options.ClientCertificate);
                httpClientHandler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => ValidateRemoteCertificate(requestMessage, certificate, chain, policyErrors);
#if NETSTANDARD
                httpClientHandler.SslProtocols = SslProtocols.Tls12;
#else
                httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
#endif
            }

            _httpClient = new HttpClientNet(httpClientHandler)
            {
                BaseAddress = new Uri(_options.BaseUrl),
                Timeout = TimeSpan.FromSeconds(_options.Timeout)
            };
        }

        /// <summary>
        /// Configuration options for http client.
        /// </summary>
        protected HttpClientOptions Options => _options;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (disposing)
            {
                _httpClient.Dispose();
                _httpClient = null;
                _options = null;
            }

            _disposed = true;
        }
        /// <summary>
        /// Send a request to a remote endpoint.
        /// </summary>
        /// <param name="httpClientRequest">
        /// Request information.
        /// </param>
        protected virtual HttpClientResponse Send(HttpClientRequest httpClientRequest)
        {
            if (httpClientRequest == null)
            {
                throw new ArgumentException("Request configuration cannot be null", nameof(httpClientRequest));
            }

            _httpClient.DefaultRequestHeaders.Clear();

            if (httpClientRequest.Cookies != null && httpClientRequest.Cookies.Any())
            {
                var cookies = httpClientRequest.Cookies
                                               .Select(x => x.Name + "=" + x.Value)
                                               .ToArray();

                _httpClient.DefaultRequestHeaders.Add("Cookie", string.Join(";", cookies));
            }

            if (httpClientRequest.Headers != null && httpClientRequest.Headers.Any())
            {
                foreach (var header in httpClientRequest.Headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = httpClientRequest.Method,
                RequestUri = httpClientRequest.BuildUri(Options.BaseUrl)
            };

            if (!string.IsNullOrEmpty(httpClientRequest.Body))
            {
                httpRequestMessage.Content = new StringContent(httpClientRequest.Body, Encoding.UTF8, httpClientRequest.ContentType);
            }

            HttpClientResponse httpClientResponse = null;
            var sendTask = _httpClient.SendAsync(httpRequestMessage);

            try
            {
                sendTask.Wait();
                httpClientResponse = sendTask.Result.ToHttpClientResponse();
            }
            catch (AggregateException ex) when (ex.InnerException is HttpRequestException)
            {
                httpClientResponse = new HttpClientResponse
                {
                    Message = "Service Unavailable",
                    StatusCode = HttpStatusCode.ServiceUnavailable
                };
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                httpClientResponse = new HttpClientResponse
                {
                    Message = "Request Timeout",
                    StatusCode = HttpStatusCode.RequestTimeout
                };
            }

            return httpClientResponse;
        }
        /// <summary>
        /// Validate a remote certificate.
        /// </summary>
        /// <param name="httpRequestMessage">
        /// Request message information.
        /// </param>
        /// <param name="certificate">
        /// Certificate to validate.
        /// </param>
        /// <param name="chain">
        /// Chain building engine.
        /// </param>
        /// <param name="sslPolicyErrors">
        /// Errors detected by SSL policy.
        /// </param>
        protected virtual Boolean ValidateRemoteCertificate(HttpRequestMessage httpRequestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}