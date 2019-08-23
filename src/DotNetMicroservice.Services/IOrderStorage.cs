using System.Threading.Tasks;
using DotNetMicroservice.Domain;

namespace DotNetMicroservice.Processes
{
    public interface IOrderStorage
    {
        Task<Order> ReadOrderAsync(string id);

        Task WriteOrderAsync(Order order);

        Task<Order[]> ReadOrdersAsync(string userId);
    }
}