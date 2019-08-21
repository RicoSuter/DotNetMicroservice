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
    public class CreateSubscriptionMessageProcessorService : BackgroundService
    {
        private readonly IMessageReceiver<CreateSubscriptionMessage> _messageReceiver;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger _logger;

        public CreateSubscriptionMessageProcessorService(IMessageReceiver<CreateSubscriptionMessage> messageReceiver, ISubscriptionService subscriptionService, ILogger logger)
        {
            _messageReceiver = messageReceiver;
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageReceiver.ListenAndDeserializeJsonAsync(async (messages, ct) =>
            {
                foreach (var m in messages)
                {
                    try
                    {
                        var message = m.Object;
                        await _subscriptionService.CreateOrExtendAsync(message.SubscriptionId, message.UserId, message.ProductId);
                        await _messageReceiver.ConfirmAsync(m, ct);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Error while processing {nameof(CreateSubscriptionMessage)} message.");
                        await _messageReceiver.RejectAsync(m, ct);
                    }
                }
            }, stoppingToken);
        }
    }
}
