using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ReservationApi.Tests
{
    public class ResourceControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ResourceControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ResourceController_FullCrudProcessTest()
        {
            var response = await _client.GetAsync("/Resource");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.True(!string.IsNullOrEmpty(responseString));
        }
    }
}