using DotNetMicroservice.Domain;
using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IProductService _productService;

        public OrderService(IProductService productService, IOrderStorage orderStorage)
        {
            _productService = productService;
            _orderStorage = orderStorage;
        }

        public async Task CreateOrderAsync(string id, string userId, string productId, int quantity)
        {
            var successful = await _productService.RemoveFromStockAsync(productId, quantity);
            if (successful)
            {
                await _orderStorage.WriteOrderAsync(new Order
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
                await _orderStorage.WriteOrderAsync(new Order
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
            var order = await _orderStorage.ReadOrderAsync(id);
            order.State = OrderState.Completed;
            order.ParcelNumber = parcelNumber;

            await _orderStorage.WriteOrderAsync(order);
        }
    }
}