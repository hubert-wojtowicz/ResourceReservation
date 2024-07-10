namespace ReservationApi.ApiContract
{
    public class CreateReservationRequest
    {
        public Guid ResourceId { get; set; }
        public Guid ReservingPartyId { get; set; }
    }
}
