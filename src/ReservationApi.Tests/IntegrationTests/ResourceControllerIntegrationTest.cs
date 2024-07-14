using ReservationApi.ApiContract;
using ReservationApi.Infrastructure.Entities;
using System.Net.Http.Json;

namespace ReservationApi.Tests.IntegrationTests
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

        [Fact]
        public async Task FullCrud_Test()
        {
            var id = new Guid("09df374a-61c8-41d8-abcb-0b6475cedc99");
            await CleanResoure(id);
            try
            {
                var createResponse = await _client.PostAsJsonAsync("/Resource", new CreateResourceRequest
                {
                    ResourceId = id,
                    Tags = new List<string> { "Param1=A;Param2=B" }
                });

                createResponse.EnsureSuccessStatusCode();

                var getResourcee = await _client.GetAsync("/Resource?ResourceId=09df374a-61c8-41d8-abcb-0b6475cedc99");

                var body = await getResourcee.Content.ReadFromJsonAsync<IEnumerable<ResourceDbEntity>>() ?? Array.Empty<ResourceDbEntity>();

                Assert.Contains(body, x => x.ResourceId == id);
            }
            finally
            {
                await CleanResoure(id);
            }
        }

        private async Task CleanResoure(Guid id)
        {
            try
            {
                var url = $"/Resource/{id}";
                var response = await _client.DeleteAsync(url);
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}