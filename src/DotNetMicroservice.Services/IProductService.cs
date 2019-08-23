using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public interface IProductService
    {
        Task AddProduct(string id, string title, int stock);

        Task<bool> RemoveFromStockAsync(string id, int quantity);
    }
}