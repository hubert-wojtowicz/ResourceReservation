namespace ReservationApi.Infrastructure.Entities
{
    public class ResourceDbEntity
    {
        public Guid ResourceId { get; set; }
        public List<string> Tags { get; set; }
        public ReservationDbEntity? Reservation { get; set; }
    }
}