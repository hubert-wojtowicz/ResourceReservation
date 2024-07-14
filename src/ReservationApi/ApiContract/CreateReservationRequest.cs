using ReservationApi.Validations;

namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="CreateReservationRequest"/> contract model.
    /// </summary>
    public class CreateReservationRequest
    {
        /// <summary>
        /// Gets or sets the reservation identifier.
        /// </summary>
        /// <value>
        /// The reservation identifier.
        /// </value>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Resource unique identifier.
        /// </summary>
        [NotDefaultGuid]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// Party that occupy resource exclusively i.e. reserved.
        /// </summary>
        [NotDefaultGuid]
        public Guid ReservingPartyId { get; set; }
    }
}
