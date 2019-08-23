using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMicroservice.Models;
using DotNetMicroservice.Processes;
using DotNetMicroservice.Services.Messages;
using Microsoft.AspNetCore.Mvc;
using Namotion.Messaging;
using Namotion.Messaging.Abstractions;

namespace DotNetMicroservice.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMessagePublisher<CreateOrderMessage> _createOrderPublisher;
        private readonly IMessagePublisher<CompleteOrderMessage> _completeOrderPublisher;
        private readonly IOrderStorage _orderStorage;

        public OrdersController(IMessagePublisher<CreateOrderMessage> createOrderPublisher, IMessagePublisher<CompleteOrderMessage> completeOrderPublisher, IOrderStorage orderStorage)
        {
            _createOrderPublisher = createOrderPublisher;
            _completeOrderPublisher = completeOrderPublisher;
            _orderStorage = orderStorage;
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<OrderDto>> GetOrders(string userId)
        {
            var orders = await _orderStorage.ReadOrdersAsync(userId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                ProductId = o.ProductId,
                Quantity = o.Quantity,
                ParcelNumber = o.ParcelNumber,
                State = o.State.ToString()
            });
        }

        [HttpPost("{userId}")]
        public async Task<string> CreateOrder(string userId, [FromBody] OrderDto order)
        {
            // ## Messaging: Publisher

            var id = Guid.NewGuid().ToString();
            await _createOrderPublisher.PublishAsJsonAsync(new CreateOrderMessage
            {
                Id = id,
                UserId = userId,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            });

            return id;
        }

        [HttpPut("{orderId}")]
        public async Task CompleteOrder(string orderId, [FromBody] string parcelNumber)
        {
            // ## Messaging: Publisher
            await _completeOrderPublisher.PublishAsJsonAsync(new CompleteOrderMessage
            {
                Id = orderId,
                ParcelNumber = parcelNumber
            });
        }
    }
}
