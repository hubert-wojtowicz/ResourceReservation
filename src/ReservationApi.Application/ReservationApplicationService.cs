using Microsoft.EntityFrameworkCore;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure;
using ReservationApi.Infrastructure.Entities;
using System.Text.Json.Serialization;

namespace ReservationApi.Application
{
    public interface IReservationApplicationService
    {
        Task<OperationResult<ReservationDbEntity>> CreateResevation(CreateReservationDto createReservationDto);
    }

    public class ReservationApplicationService : IReservationApplicationService
    {
        private readonly IRepository<ReservationDbEntity> _reaservationRepository;
        private readonly IRepository<ResourceDbEntity> _resourceRepository;

        public ReservationApplicationService(
            IRepository<ReservationDbEntity> reaservationRepository,
            IRepository<ResourceDbEntity> resourceRepository)
        {
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
            catch (DbUpdateException)
            {
                return new ErrorModel($"Reservation for {nameof(createReservationDto.ResourceId)} already exist.", new List<KeyValuePair<string, string>> 
                { 
                    KeyValuePair.Create(nameof(createReservationDto.ResourceId), createReservationDto.ResourceId.ToString()) 
                });
            }

            return OperationResult<ReservationDbEntity>.Success(entity);
        }
    }

    public class CreateReservationDto
    {
        public Guid ReservingPartyId { get; set; }
        public Guid ReservationId { get; set; }
        public Guid ResourceId { get; set; }
    }


    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ErrorModel? Error { get; }

        protected OperationResult(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        protected OperationResult(ErrorModel error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static OperationResult<T> Success(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
            }

            return new OperationResult<T>(value);
        }

        public static OperationResult<T> Failure(ErrorModel error)
        {
            if (string.IsNullOrEmpty(error.Message))
            {
                throw new ArgumentNullException(nameof(error), "Error message cannot be null or empty.");
            }

            return new OperationResult<T>(error);
        }

        public static implicit operator OperationResult<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator OperationResult<T>(ErrorModel error)
        {
            return Failure(error);
        }
    }

    public record ErrorModel(string Message, List<KeyValuePair<string, string>> Metadata);
}
