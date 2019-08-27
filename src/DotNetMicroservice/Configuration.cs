using DotNetMicroservice.Processes;
using DotNetMicroservice.Repositories;
using DotNetMicroservice.Repositories.Interfaces;
using DotNetMicroservice.Services;
using DotNetMicroservice.Services.Interfaces;
using DotNetMicroservice.Services.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Namotion.Messaging;
using Namotion.Messaging.Abstractions;

namespace DotNetMicroservice
{
    public static class Configuration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IOrderRepository, InMemoryOrderRepository>();
            services.TryAddSingleton<IProductService, ProductService>();
            services.TryAddSingleton<IOrderService, OrderService>();
            return services;
        }

        public static IServiceCollection AddCreateOrderMessagePublisherReceiver(this IServiceCollection services)
        {
            var publisherReceiver = InMemoryMessagePublisherReceiver.Create();
            services.TryAddSingleton(((IMessagePublisher)publisherReceiver).WithMessageType<CreateOrderMessage>());
            services.TryAddSingleton(((IMessageReceiver)publisherReceiver).WithMessageType<CreateOrderMessage>());
            return services;
        }

        public static IServiceCollection AddCompleteOrderMessagePublisherReceiver(this IServiceCollection services)
        {
            var publisherReceiver = InMemoryMessagePublisherReceiver.Create();
            services.TryAddSingleton(((IMessagePublisher)publisherReceiver).WithMessageType<CompleteOrderMessage>());
            services.TryAddSingleton(((IMessageReceiver)publisherReceiver).WithMessageType<CompleteOrderMessage>());
            return services;
        }

        public static IServiceCollection AddCreateOrderMessageProcessor(this IServiceCollection services)
        {
            return services
                .AddBusinessServices()
                .AddCreateOrderMessagePublisherReceiver()
                .AddHostedService<CreateOrderMessageProcessor>();
        }

        public static IServiceCollection AddCompleteOrderMessageProcessor(this IServiceCollection services)
        {
            return services
                .AddBusinessServices()
                .AddCompleteOrderMessagePublisherReceiver()
                .AddHostedService<CompleteOrderMessageProcessor>();
        }
    }
}
