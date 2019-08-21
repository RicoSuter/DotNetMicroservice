using DotNetMicroservice.Client;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetMicroservice.Tests
{
    public class SubscriptionsControllerTests : IntegrationTestsBase
    {
        [Fact]
        public async Task WhenGetAllIsCalled_ThenValuesAreReturned()
        {
            // Arrange
            using (var factory = CreateInMemoryWebApplicationFactory())
            {
                var httpClient = factory.CreateClient();
                var client = new SubscriptionsClient(httpClient);

                // Act
                var values = await client.GetAllAsync();

                // Assert
                Assert.True(values.Any());
            }
        }
    }
}
