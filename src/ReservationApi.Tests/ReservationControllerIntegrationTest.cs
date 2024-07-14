using ReservationApi.ApiContract;
using ReservationApi.Application.Models;
using System.Net.Http.Json;

namespace ReservationApi.Tests
{
    public class ReservationControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ReservationControllerIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateReservation_WhenResourceDoesNotExist_ReturnsBadRequest()
        {
            var reservationId = new Guid("09df374a-61c8-41d8-abcb-0b6475cedc91");
            var reservingPartyId = new Guid("b7109a4a-fad8-4a74-b184-5176a363deb8");
            var resourceId = new Guid("a3dcd782-dbaf-4aa7-b965-ab1917d15af6");

            await Clean("Resource", resourceId);

            var createReservation1Response = await _client.PostAsJsonAsync("/Reservation", new CreateReservationRequest
            {
                ReservationId = reservationId,
                ResourceId = resourceId,
                ReservingPartyId = reservingPartyId
            });

            var error = await createReservation1Response.Content.ReadFromJsonAsync<ErrorModel>();

            Assert.True(error != null);
            Assert.True(error.Message == "Resource you trying to reserve does not exist.");
            Assert.True(error.Metadata.Count > 0);
        }

        [Fact]
        public async Task CreateReservation_WhenAttemptToReserveSameResourceTwice_ReturnsBadRequest()
        {
            var reservation1Id = new Guid("43340080-d16a-42f1-8b51-f7f38e1d983e");
            var reservation2Id = new Guid("8e8631aa-0072-41fb-bc1b-60eaeca2cd9d");
            var resourceId = new Guid("9cefcbb0-e62c-4788-8a19-62cc63375c0c");
            var reservingPartyId = new Guid("b7109a4a-fad8-4a74-b184-5176a363deb8");

            await Clean("Resource", resourceId);
            await Clean("Reservation", reservation1Id);
            await Clean("Reservation", reservation2Id);

            try
            {
                var createResourceResponse = await _client.PostAsJsonAsync("/Resource", new CreateResourceRequest
                {
                    ResourceId = resourceId,
                    Tags = new List<string> { "Param1=A;Param2=B" }
                });
                createResourceResponse.EnsureSuccessStatusCode();

                // First reservation
                var createReservation1Response = await _client.PostAsJsonAsync("/Reservation", new CreateReservationRequest
                {
                    ReservationId = reservation1Id,
                    ResourceId = resourceId,
                    ReservingPartyId = reservingPartyId
                });
                createReservation1Response.EnsureSuccessStatusCode();
                // Second reservation
                var createReservation2Response = await _client.PostAsJsonAsync("/Reservation", new CreateReservationRequest
                {
                    ReservationId = reservation2Id,
                    ResourceId = resourceId,
                    ReservingPartyId = reservingPartyId
                });
                var resonse2 = await createReservation2Response.Content.ReadFromJsonAsync<ErrorModel>();

                Assert.True(createReservation1Response.IsSuccessStatusCode);
                Assert.True(!createReservation2Response.IsSuccessStatusCode);
                Assert.True(createReservation2Response.StatusCode == System.Net.HttpStatusCode.BadRequest);
                Assert.True(resonse2 != null);
                Assert.True(resonse2.Message != null);
                Assert.True(resonse2.Message == "Reservation for ResourceId already exist.");
            }
            finally
            {
                await Clean("Resource", resourceId);
                await Clean("Reservation", reservation1Id);
                await Clean("Reservation", reservation2Id);
            }
        }

        private async Task Clean(string route, Guid id)
        {
            try
            {
                var url = $"/{route}/{id}";
                var response = await _client.DeleteAsync(url);
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}