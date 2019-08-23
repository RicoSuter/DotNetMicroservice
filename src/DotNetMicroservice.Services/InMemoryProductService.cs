using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public class InMemoryProductService : IProductService
    {
        public Task AddProduct(string id, string title, int stock)
        {
            return Task.CompletedTask;
        }

        public Task<bool> RemoveFromStockAsync(string id, int quantity)
        {
            return Task.FromResult(true);
        }
    }
}