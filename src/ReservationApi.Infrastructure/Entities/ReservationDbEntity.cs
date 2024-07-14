using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Infrastructure
{
    public class ReservationDbEntity
    {
        public Guid ReservationId { get; set; }
        public Guid ReservingPartyId { get; set; }
        public DateTime ReservedAt { get; set; }
        public Guid? ResourceId { get; set; }
        public ResourceDbEntity? Resource { get; set; }
    }
}