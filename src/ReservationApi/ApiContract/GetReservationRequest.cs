namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="GetReservationRequest"/> contract model.
    /// </summary>
    public class GetReservationRequest : IPaginable
    {
        /// <inheritdoc/>
        public int Take { get; set; } = 10;

        /// <inheritdoc/>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// Id of reservation.
        /// </summary>
        public Guid? ReservationId { get; set; }

        /// <summary>
        /// Reservation tags.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Party that have reserved Resource.
        /// </summary>
        public Guid? PartyOwnerId { get; set; }
    }
}
