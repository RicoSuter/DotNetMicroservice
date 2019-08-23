using System;
using System.Net.Http;

namespace DotNetMicroservice.Tests.Common
{
    public class ExternalWebApplication : IWebApplication
    {
        private HttpClient _httpClient;

        public ExternalWebApplication(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ExternalWebApplication(Uri baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = baseAddress };
        }

        public HttpClient CreateClient()
        {
            return _httpClient;
        }

        public void Dispose()
        {
        }
    }
}
