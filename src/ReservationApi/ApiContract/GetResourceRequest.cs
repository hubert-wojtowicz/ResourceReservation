namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="GetResourceRequest"/> contract model.
    /// </summary>
    /// <seealso cref="ReservationApi.ApiContract.IPaginable" />
    public class GetResourceRequest : IPaginable
    {
        /// <inheritdoc/>
        public int Take { get; set; }

        /// <inheritdoc/>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public Guid ResourceId { get; set; }
    }
}