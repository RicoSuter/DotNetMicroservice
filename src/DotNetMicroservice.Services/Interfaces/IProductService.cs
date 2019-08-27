using System.Threading.Tasks;

namespace DotNetMicroservice.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> RemoveFromStockAsync(string id, int quantity);
    }
}