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

        public Task<Order[]> ReadOrdersAsync(string userId)
        {
            return Task.FromResult(_orders.Where(o => o.UserId == userId).ToArray());
        }

        public Task WriteOrderAsync(Order order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }
    }
}