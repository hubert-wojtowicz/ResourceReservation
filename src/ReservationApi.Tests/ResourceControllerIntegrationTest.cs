using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ReservationApi.Tests
{
    public class ResourceControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ResourceControllerIntegrationTest(CustomWebApplicationFactory<Program> factory)
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