using DotNetMicroservice.Client;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetMicroservice.Tests
{
    public class OrdersControllerTests : IntegrationTestsBase
    {
        [Fact]
        public async Task WhenOrderIsCreated_ThenOrderIsProcessed()
        {
            // Arrange
            using (var factory = CreateInMemoryWebApplicationFactory())
            {
                var httpClient = factory.CreateClient();
                var client = new OrdersClient(httpClient);

                var userId = "myUser";

                // Act
                var id = await client.CreateOrderAsync(userId, new OrderDto { ProductId = "foo", Quantity = 5 });
                var orders = await WaitForResultAsync(async () =>
                {
                    return await client.GetOrdersAsync(userId);
                }, e => e.Any());

                // Assert
                Assert.Contains(orders, o => o.Id == id);
            }
        }
    }
}
