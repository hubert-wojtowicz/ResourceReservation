namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="CreateResourceRequest"/> contract model.
    /// </summary>
    public class CreateResourceRequest
    {
        /// <summary>
        /// Resource unique identifier.
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// List of tags
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();
    }
}
