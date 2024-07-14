using ReservationApi.Application.Models;
using ReservationApi.Infrastructure;

namespace ReservationApi.Application
{
    public interface IReservationApplicationService
    {
        Task<OperationResult<ReservationDbEntity>> CreateResevation(CreateReservationDto createReservationDto);
    }
}