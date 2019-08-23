using DotNetMicroservice.Client;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetMicroservice.Tests
{
    public class OrdersControllerTests : IntegrationTestsBase
    {
        [Fact]
        public async Task WhenOrderIsCreatedAndCompleted_ThenParcelNumberIsAvailable()
        {
            // Arrange
            using (var factory = CreateInMemoryWebApplicationFactory())
            {
                var httpClient = factory.CreateClient();
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
