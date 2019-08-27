using DotNetMicroservice.Domain;
using DotNetMicroservice.Repositories.Interfaces;
using DotNetMicroservice.Services.Interfaces;
using System.Threading.Tasks;

namespace DotNetMicroservice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IProductService productService, IOrderRepository orderRepository)
        {
            _productService = productService;
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(string id, string userId, string productId, int quantity)
        {
            var successful = await _productService.RemoveFromStockAsync(productId, quantity);
            if (successful)
            {
                await _orderRepository.UpsertOrderAsync(new Order
                {
                    Id = id,
                    State = OrderState.Pending,
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                });
            }
            else
            {
                await _orderRepository.UpsertOrderAsync(new Order
                {
                    Id = id,
                    State = OrderState.NotInStock,
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                });
            }
        }

        public async Task CompleteOrderAsync(string id, string parcelNumber)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            order.State = OrderState.Completed;
            order.ParcelNumber = parcelNumber;

            await _orderRepository.UpsertOrderAsync(order);
        }
    }
}