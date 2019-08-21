using DotNetMicroservice.Domain;
using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderStorage _orderStorage;

        public OrderService(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }

        public async Task CreateOrderAsync(string id, string userId, string productId, int quantity)
        {
            await _orderStorage.WriteOrderAsync(new Order
            {
                Id = id,
                ProductId = productId,
                UserId = userId, 
                Quantity = quantity
            });
        }
    }
}