using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string id, string userId, string productId, int quantity);

        Task CompleteOrderAsync(string id, string parcelNumber);
    }
}