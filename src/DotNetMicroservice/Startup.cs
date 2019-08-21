using DotNetMicroservice.Processes;
using DotNetMicroservice.Services.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Namotion.Messaging;
using Namotion.Messaging.Abstractions;

namespace DotNetMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register business services
            services.AddSingleton<IOrderStorage, InMemoryOrderStorage>();
            services.AddSingleton<IOrderService, OrderService>();

            // ## NSwag
            services.AddOpenApiDocument(c => c.Title = "DotNetMicroservice");

            // ## Messaging
            var message = InMemoryMessagePublisherReceiver.Create();
            services.AddSingleton(((IMessagePublisher)message).WithMessageType<CreateOrderMessage>());
            services.AddSingleton(((IMessageReceiver)message).WithMessageType<CreateOrderMessage>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // ## NSwag
            app.UseOpenApi();
            app.UseSwaggerUi3();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
