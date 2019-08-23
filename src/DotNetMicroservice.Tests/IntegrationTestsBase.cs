using DotNetMicroservice.Processes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetMicroservice.Tests
{
    public abstract class IntegrationTestsBase
    {
        protected WebApplicationFactory<Startup> CreateInMemoryWebApplicationFactory()
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    services.AddHostedService<CreateOrderMessageProcessorService>();
                    services.AddHostedService<CompleteOrderMessageProcessorService>();
                }));
        }
    }
}
