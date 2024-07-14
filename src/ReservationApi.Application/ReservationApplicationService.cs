using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservationApi.Application.Models;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure;
using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Application
{
    public class ReservationApplicationService : IReservationApplicationService
    {
        private readonly ILogger<ReservationApplicationService> _logger;
        private readonly IRepository<ReservationDbEntity> _reaservationRepository;
        private readonly IRepository<ResourceDbEntity> _resourceRepository;

        public ReservationApplicationService(
            ILogger<ReservationApplicationService> logger,
            IRepository<ReservationDbEntity> reaservationRepository,
            IRepository<ResourceDbEntity> resourceRepository)
        {
            _logger = logger;
            _reaservationRepository = reaservationRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<OperationResult<ReservationDbEntity>> CreateResevation(CreateReservationDto createReservationDto)
        {
            var resource = await _resourceRepository.Get(createReservationDto.ResourceId);

            if (resource == null)
            {
                return new ErrorModel($"Resource you trying to reserve does not exist.", new List<KeyValuePair<string, string>>
                {
                    KeyValuePair.Create(nameof(createReservationDto.ResourceId), createReservationDto.ResourceId.ToString())
                });
            }

            ReservationDbEntity entity;
            try
            {
                // ooptimistic approch for overreserving. Unique index on db will ensure overreserving will not happen.
                entity = new ReservationDbEntity
                {
                    ReservationId = createReservationDto.ReservationId,
                    ResourceId = createReservationDto.ResourceId,
                    ReservedAt = DateTime.UtcNow,
                    ReservingPartyId = createReservationDto.ReservingPartyId
                };
                await _reaservationRepository.Add(entity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message); // Just to know reason

                return new ErrorModel($"Reservation for {nameof(createReservationDto.ResourceId)} already exist.", new List<KeyValuePair<string, string>> 
                { 
                    KeyValuePair.Create(nameof(createReservationDto.ResourceId), createReservationDto.ResourceId.ToString()) 
                });
            }

            return OperationResult<ReservationDbEntity>.Success(entity);
        }
    }
}
