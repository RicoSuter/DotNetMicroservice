using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMicroservice.Domain;

namespace DotNetMicroservice.Processes
{
    public class InMemoryOrderStorage : IOrderStorage
    {
        private readonly List<Order> _orders;

        public InMemoryOrderStorage()
        {
            _orders = new List<Order>();
        }

        public async Task<Order> ReadOrderAsync(string id)
        {
            await Task.Delay(500);
            return _orders.First(o => o.Id == id);
        }

        public async Task<Order[]> ReadOrdersAsync(string userId)
        {
            await Task.Delay(500);
            return _orders.Where(o => o.UserId == userId).ToArray();
        }

        public async Task WriteOrderAsync(Order order)
        {
            await Task.Delay(1000);
            _orders.Add(order);
        }
    }
}