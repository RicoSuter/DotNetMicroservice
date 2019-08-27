using System.Threading.Tasks;

namespace DotNetMicroservice.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string id, string userId, string productId, int quantity);

        Task CompleteOrderAsync(string id, string parcelNumber);
    }
}