using DotNetMicroservice.Services.Interfaces;
using System.Threading.Tasks;

namespace DotNetMicroservice.Services
{
    public class ProductService : IProductService
    {
        public Task<bool> RemoveFromStockAsync(string id, int quantity)
        {
            return Task.FromResult(true);
        }
    }
}