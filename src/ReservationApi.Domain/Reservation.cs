namespace ReservationApi.Domain
{
    /// <summary>
    /// Reservation is an object answering to question how much 
    /// </summary>
    public class Reservation
    {
        public Guid ReservationId { get; }
        public DateTime CreatedAt { get; }
        public Resource Resource { get; }

        protected Reservation(Resource resource, Guid reservationId)
        {
            Resource = resource;
            ReservationId = reservationId;
            CreatedAt = DateTime.UtcNow;
        }


        public static Reservation Create(Resource resource, Guid? reservationId)
        {
            return new Reservation(resource, reservationId ?? Guid.NewGuid());
        }
    }

    public record Resource(Guid ResourceId);

    public record Party(Guid partyId);
}
