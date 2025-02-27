using Accelerate.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Accelerate.Web.Extensions
{
    /// <summary>
    /// Extensions class for HttpResponseMessage class.
    /// </summary>
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Read the content of http response message and create a HttpClientResponse class.
        /// </summary>
        /// <param name="httpResponseMessage">
        /// Http response message.
        /// </param>
        internal static HttpClientResponse ToHttpClientResponse(this HttpResponseMessage httpResponseMessage)
        {
            var httpClientResponse = new HttpClientResponse();
            var readTask = httpResponseMessage.Content.ReadAsStringAsync();

            readTask.Wait();

            httpClientResponse.Body = readTask.Result;
            httpClientResponse.Headers = new Dictionary<String, String>();

            foreach (var header in httpResponseMessage.Headers)
            {
                if (header.Value.Count() > 1)
                {
                    var headerValue = String.Join("; ", header.Value);
                    httpClientResponse.Headers.Add(header.Key, headerValue);
                }
                else
                {
                    var headerValue = header.Value.FirstOrDefault();
                    httpClientResponse.Headers.Add(header.Key, headerValue);
                }
            }

            httpClientResponse.Message = httpResponseMessage.ReasonPhrase;
            httpClientResponse.StatusCode = httpResponseMessage.StatusCode;

            return httpClientResponse;
        }
    }
}