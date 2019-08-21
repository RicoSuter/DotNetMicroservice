using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetMicroservice.Tests
{
    public abstract class IntegrationTestsBase
    {
        protected WebApplicationFactory<Startup> CreateInMemoryWebApplicationFactory()
        {
            var factory = new WebApplicationFactory<Startup>();
            return factory
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    // TODO: Override test services
                }));
        }
    }
}
