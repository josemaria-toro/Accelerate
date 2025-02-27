using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Threading;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Context for tests based on HttpClient class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HttpClientTestContext : xUnitTestContext
    {
        private HttpClientClass _httpClient;
        private HttpListener _httpListener;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        public HttpClientTestContext()
        {
            var random = new Random();
            var port = random.Next(9000, 9999);
            var baseUrl = $"http://localhost:{port}/";

            if (_httpClient == null)
            {
                var httpClientOptions = Options.Create(new HttpClientOptions
                {
                    BaseUrl = baseUrl,
                    Timeout = 5
                });

                _httpClient = new HttpClientClass(httpClientOptions);
            }

            if (_httpListener == null)
            {
                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add(baseUrl);

                Debug.WriteLine("Starting server...");

                foreach (var prefix in _httpListener.Prefixes)
                {
                    Debug.WriteLine($"Listening on '{prefix}'");
                }

                _httpListener.Start();
                _httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), _httpListener);
            }
        }

        internal HttpClientClass HttpClient => _httpClient;

        private static void ListenerCallback(IAsyncResult asyncResult)
        {
            var httpListener = (HttpListener)asyncResult.AsyncState;
            var httpContext = httpListener.EndGetContext(asyncResult);
            var httpRequest = httpContext.Request;
            var httpRequestBuffer = new Byte[httpRequest.ContentLength64];
            var httpRequestStream = httpRequest.InputStream;

            httpRequestStream.Read(httpRequestBuffer, 0, httpRequestBuffer.Length);

            var httpRequestString = Encoding.UTF8.GetString(httpRequestBuffer);

            Debug.WriteLine($"Request received: {httpRequestString}");

            if (httpRequestString != "Shutdown")
            {
                httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), httpListener);
            }

            if (httpRequestString == "408")
            {
                Debug.WriteLine("Forcing a timeout error...");
                Thread.Sleep(10000);
            }
            else if (httpRequestString == "Shutdown")
            {
                Debug.WriteLine("Shuting down the server...");
                httpListener.Close();
            }
            else
            {
                var httpResponse = httpContext.Response;

                httpResponse.StatusCode = Int32.Parse(httpRequestString);
                httpResponse.StatusDescription = Enum.GetName(typeof(HttpStatusCode), Int32.Parse(httpRequestString));
                httpResponse.Close();
            }
        }
    }
}