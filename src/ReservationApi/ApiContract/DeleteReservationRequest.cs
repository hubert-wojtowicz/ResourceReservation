namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="DeleteReservationRequest"/> contract model.
    /// </summary>
    public class DeleteReservationRequest
    {
        /// <summary>
        /// Unique identifier for Reservation.
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
