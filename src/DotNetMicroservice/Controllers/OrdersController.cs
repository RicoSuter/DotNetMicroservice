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
        private readonly IMessagePublisher<CreateOrderMessage> _orderMessagePublisher;
        private readonly IOrderStorage _orderStorage;

        public OrdersController(IMessagePublisher<CreateOrderMessage> orderMessagePublisher, IOrderStorage orderStorage)
        {
            _orderMessagePublisher = orderMessagePublisher;
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
                Quantity = o.Quantity
            });
        }

        [HttpPost("{userId}")]
        public async Task<string> CreateOrder(string userId, [FromBody] OrderDto order)
        {
            var id = Guid.NewGuid().ToString();

            // ## Messaging: Publisher
            await _orderMessagePublisher.PublishAsJsonAsync(new CreateOrderMessage
            {
                Id = id,
                UserId = userId,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            });

            return id;
        }
    }
}
