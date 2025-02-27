using Accelerate.Web.Http;
using System;
using System.Linq;

namespace Accelerate.Web.Extensions
{
    /// <summary>
    /// Extensions class for HttpClientRequest class.
    /// </summary>
    internal static class HttpClientRequestExtensions
    {
        /// <summary>
        /// Build a full uri using the information of http client request Object.
        /// </summary>
        /// <param name="httpClientRequest">
        /// Http client request information.
        /// </param>
        /// <param name="baseUrl">
        /// Base url.
        /// </param>
        internal static Uri BuildUri(this HttpClientRequest httpClientRequest, String baseUrl)
        {
            var uriBuilder = new UriBuilder(baseUrl);

            if (httpClientRequest.Parameters != null && httpClientRequest.Parameters.Any())
            {
                var paramsArray = httpClientRequest.Parameters.Select(x => $"{x.Key}={x.Value}")
                                                              .ToArray();

                var queryParams = String.Join("&", paramsArray);

                if (String.IsNullOrEmpty(uriBuilder.Query))
                {
                    uriBuilder.Query = queryParams;
                }
                else
                {
                    uriBuilder.Query = $"{uriBuilder.Query}&{queryParams}";
                }
            }

            if (String.IsNullOrEmpty(uriBuilder.Path))
            {
                uriBuilder.Path = httpClientRequest.Path;
            }
            else if (uriBuilder.Path.EndsWith("/"))
            {
                uriBuilder.Path = $"{uriBuilder.Path}{httpClientRequest.Path}";
            }
            else
            {
                uriBuilder.Path = $"{uriBuilder.Path}/{httpClientRequest.Path}";
            }

            uriBuilder.Path = uriBuilder.Path.Replace("//", "/");

            return uriBuilder.Uri;
        }
    }
}