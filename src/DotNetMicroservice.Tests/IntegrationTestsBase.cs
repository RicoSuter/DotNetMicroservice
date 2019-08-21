using DotNetMicroservice.Processes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
                    services.AddHostedService<CreateOrderMessageProcessorService>();
                }));
        }

        protected async Task<TResult> WaitForResultAsync<TResult>(Func<Task<TResult>> function, Func<TResult, bool> predicate)
        {
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(1000);

                var result = await function();
                if (predicate(result))
                {
                    return result;
                }
            }

            throw new Exception("The test timed out.");
        }
    }
}
