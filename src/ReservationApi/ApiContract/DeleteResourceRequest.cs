using Microsoft.AspNetCore.Mvc;
using ReservationApi.Validations;

namespace ReservationApi.ApiContract
{
    /// <summary>
    /// <see cref="DeleteResourceRequest"/> contract model.
    /// </summary>
    public class DeleteResourceRequest
    {
        /// <summary>
        /// Unique resource identifier.
        /// </summary>
        [NotDefaultGuid]
        [FromRoute(Name = "id")]
        public Guid ResourceId { get; set; }
    }
}
