using System.Threading.Tasks;

namespace DotNetMicroservice.Processes
{
    public interface ISubscriptionService
    {
        Task CreateOrExtendAsync(string subscriptionId, string userId, string productId);
    }
}