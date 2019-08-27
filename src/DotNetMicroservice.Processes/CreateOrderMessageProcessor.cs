using DotNetMicroservice.Services.Interfaces;
using DotNetMicroservice.Services.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Namotion.Messaging;
using Namotion.Messaging.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    // ## Messaging: Receiver

    public class CreateOrderMessageProcessor : BackgroundService
    {
        private readonly IMessageReceiver<CreateOrderMessage> _messageReceiver;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public CreateOrderMessageProcessor(
            IMessageReceiver<CreateOrderMessage> messageReceiver,
            IOrderService orderService,
            ILogger<CreateOrderMessageProcessor> logger)
        {
            _messageReceiver = messageReceiver;
            _orderService = orderService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Now listening for {nameof(CreateOrderMessage)} messages.");

            await _messageReceiver.ListenAndDeserializeJsonAsync(async (messages, ct) =>
            {
                foreach (var m in messages)
                {
                    try
                    {
                        var message = m.Object;
                        await _orderService.CreateOrderAsync(message.Id, message.UserId, message.ProductId, message.Quantity);
                        await _messageReceiver.ConfirmAsync(m, ct);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Error while processing {nameof(CreateOrderMessage)} message.");
                        await _messageReceiver.RejectAsync(m, ct);
                    }
                }
            }, stoppingToken);
        }
    }
}
