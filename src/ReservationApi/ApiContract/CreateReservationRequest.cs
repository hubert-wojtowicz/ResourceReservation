namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="CreateReservationRequest"/> contract model.
    /// </summary>
    public class CreateReservationRequest
    {
        /// <summary>
        /// Resource unique identifier.
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// Party that occupy resource exclusively i.e. reserved.
        /// </summary>
        public Guid ReservingPartyId { get; set; }
    }
}
