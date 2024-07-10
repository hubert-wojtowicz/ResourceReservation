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
        public async Task Delete_WhenResourceDoesNotExist_ThenReturnNotFound404()
        {
            var url = $"/Resource/{new Guid("8324d4c8-ed2f-4a68-a4f4-fd809323dd60")}";
            var response = await _client.DeleteAsync(url);
            
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        //TODO
    }
}