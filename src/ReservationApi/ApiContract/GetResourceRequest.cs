using ReservationApi.Infrastructure;

namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="GetResourceRequest"/> contract model.
    /// </summary>
    /// <seealso cref="Infrastructure.IPaginable" />
    public class GetResourceRequest : IPaginable
    {
        /// <inheritdoc/>
        public int Take { get; set; } = 10;

        /// <inheritdoc/>
        public int Skip { get; set; } = 0;

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