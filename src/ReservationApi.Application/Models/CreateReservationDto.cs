namespace ReservationApi.Application.Models
{
    public record CreateReservationDto(Guid ReservingPartyId, Guid ReservationId, Guid ResourceId);
}
