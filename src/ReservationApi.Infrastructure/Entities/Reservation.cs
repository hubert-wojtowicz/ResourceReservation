namespace ReservationApi.Infrastructure
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public Guid ReservingPartyId { get; set; }
        public DateTime ReservedAt { get; set; }
        public Resource Resource { get; set; }
    }
}