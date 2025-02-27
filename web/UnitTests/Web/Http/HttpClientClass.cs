using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

namespace Accelerate.Web.Http
{
    [ExcludeFromCodeCoverage]
    internal class HttpClientClass : HttpClient
    {
        internal HttpClientClass(IOptions<HttpClientOptions> options) : base(options)
        {
        }

        internal HttpClientResponse Send(Int32 code)
        {
            return Send(new HttpClientRequest
            {
                Body = $"{code}",
                ContentType = "application/json",
                Cookies = [new Cookie("Cookie", "Cookie Value")],
                Headers = new Dictionary<String, String> { { "x-header", "Header Value" } },
                Method = HttpMethod.Get,
                Parameters = new Dictionary<String, Object> { { "parameter", "Parameter Value" } },
                Path = "/"
            });
        }
        internal void Shutdown()
        {
            Send(new HttpClientRequest
            {
                Body = "Shutdown",
                ContentType = "application/json",
                Method = HttpMethod.Get
            });
        }
    }
}
