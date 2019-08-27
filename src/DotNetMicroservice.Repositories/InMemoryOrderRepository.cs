using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMicroservice.Domain;
using DotNetMicroservice.Repositories.Interfaces;

namespace DotNetMicroservice.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;

        public InMemoryOrderRepository()
        {
            _orders = new List<Order>();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            await Task.Delay(500);
            return _orders.First(o => o.Id == id);
        }

        public async Task<Order[]> GetOrdersAsync(string userId)
        {
            await Task.Delay(500);
            return _orders.Where(o => o.UserId == userId).ToArray();
        }

        public async Task UpsertOrderAsync(Order order)
        {
            await Task.Delay(1000);
            _orders.Add(order);
        }
    }
}