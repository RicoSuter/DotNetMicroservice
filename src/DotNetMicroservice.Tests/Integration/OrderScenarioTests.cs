using DotNetMicroservice.Client;
using DotNetMicroservice.Tests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetMicroservice.Tests.Integration
{
    public class OrderScenarioTests
    {
        [Fact]
        public async Task WhenOrderIsCreatedAndProcessed_ThenParcelNumberIsAvailable()
        {
            // Arrange
            using (var webApplication = Test.CreateWebApplication())
            {
                var httpClient = webApplication.CreateClient();
                var client = new OrdersClient(httpClient);

                var userId = "myUser";
                var parcelNumber = "parcel-number-123";

                // Act
                var id = await client.CreateOrderAsync(userId, new OrderDto { ProductId = "foo", Quantity = 5 });
                await Test.WaitForAnyAsync(async () => await client.GetOrdersAsync(userId));

                await client.CompleteOrderAsync(id, parcelNumber);
                var orders = await Test.WaitForAsync(
                    async () => await client.GetOrdersAsync(userId),
                    o => o.Any(order => order.Id == id && order.State == "Completed"));

                // Assert
                Assert.Contains(orders, o => o.ParcelNumber == parcelNumber);
            }
        }
    }
}
