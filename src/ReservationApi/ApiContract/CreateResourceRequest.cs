namespace ReservationApi.ApiContract
{
    public class CreateResourceRequest
    {
        public Guid ResourceId { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
