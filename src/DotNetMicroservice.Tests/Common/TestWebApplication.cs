using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace DotNetMicroservice.Tests.Common
{
    public class TestWebApplication<TEntryPoint> : IWebApplication
        where TEntryPoint : class
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestWebApplication(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        public HttpClient CreateClient()
        {
            return _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}
