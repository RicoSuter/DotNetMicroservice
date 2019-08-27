using System.Threading.Tasks;
using DotNetMicroservice.Domain;

namespace DotNetMicroservice.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(string id);

        Task<Order[]> GetOrdersAsync(string userId);

        Task UpsertOrderAsync(Order order);
    }
}