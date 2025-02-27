using Accelerate.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Accelerate.Web.Http
{
    /// <summary>
    /// Class to perform unit tests of HttpClient class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HttpClientTest : xUnitTest<HttpClientTestContext>
    {
        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="context">
        /// Test context.
        /// </param>
        public HttpClientTest(HttpClientTestContext context) : base(context)
        {
        }

        /// <summary>
        /// Method to perform test of dispose method.
        /// </summary>
        [Fact]
        public void Dispose_Success()
        {
            using var httpClient = new HttpClientClass(Options.Create(new HttpClientOptions
            {
                BaseUrl = "http://localhost",
                Timeout = 30
            }));

            Assert.NotNull(httpClient);
        }
        /// <summary>
        /// Method to perform test of dispose method raising an exception of type ObjectDisposedException.
        /// </summary>
        [Fact]
        public void Dispose_Throwing_Exception()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                using var httpClient = new HttpClientClass(Options.Create(new HttpClientOptions
                {
                    BaseUrl = "http://localhost",
                    Timeout = 30
                }));

                httpClient.Dispose();
            });
        }
        /// <summary>
        /// Method to perform test of send method with 20x response codes.
        /// </summary>
        [Fact]
        public void Send_Success_Request_To_Get_Response_Code_20X()
        {
            var codes = new Int32[] { 200, 201, 202, 204, 205, 206 };

            foreach (var code in codes)
            {
                var httpClientResponse = Context.HttpClient.Send(code);

                Assert.Equal(code, (Int32)httpClientResponse.StatusCode);
            }
        }
        /// <summary>
        /// Method to perform test of send method with 30x response codes.
        /// </summary>
        [Fact]
        public void Send_Success_Request_To_Get_Response_Code_30X()
        {
            var codes = new Int32[] { 300, 301, 302, 307, 308 };

            foreach (var code in codes)
            {
                var httpClientResponse = Context.HttpClient.Send(code);

                Assert.Equal(code, (Int32)httpClientResponse.StatusCode);
            }
        }
        /// <summary>
        /// Method to perform test of send method with 40x response codes.
        /// </summary>
        [Fact]
        public void Send_Success_Request_To_Get_Response_Code_40X()
        {
            var codes = new Int32[] { 400, 401, 403, 404, 405, 406, 408, 409, 429 };

            foreach (var code in codes)
            {
                var httpClientResponse = Context.HttpClient.Send(code);

                Assert.Equal(code, (Int32)httpClientResponse.StatusCode);
            }
        }
        /// <summary>
        /// Method to perform test of send method with 50x response codes.
        /// </summary>
        [Fact]
        public void Send_Success_Request_To_Get_Response_Code_50X()
        {
            var codes = new Int32[] { 500, 502, 503 };

            foreach (var code in codes)
            {
                var httpClientResponse = Context.HttpClient.Send(code);

                Assert.Equal(code, (Int32)httpClientResponse.StatusCode);
            }
        }
    }
}
