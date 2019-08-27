using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetMicroservice
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddCommandLine(args)
               .AddEnvironmentVariables()
               .Build();

            var mode = configuration.GetMode();
            if (mode == "webapi" || mode == "all")
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            else
            {
                new HostBuilder()
                   .ConfigureServices((hostContext, services) =>
                   {
                       if (mode == "createorders")
                       {
                           services.AddCreateOrderMessageProcessor();
                       }
                       else if (mode == "completeorders")
                       {
                           services.AddCompleteOrderMessageProcessor();
                       }

                       services.AddLogging();
                   })
                   .Build()
                   .Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices((ctx, services) =>
                {
                    var mode = ctx.Configuration.GetMode();
                    if (mode == "all")
                    {
                        services.AddCreateOrderMessageProcessor();
                        services.AddCompleteOrderMessageProcessor();
                    }
                });

        private static string GetMode(this IConfiguration configuration)
        {
            return configuration.GetValue("Mode", "all").ToLowerInvariant();
        }
    }
}
