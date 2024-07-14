using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Application;
using ReservationApi.Application.Models;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure;
using ReservationApi.Validations;

namespace ReservationApi.Controllers;

/// <summary>
/// REST controller over Reservation.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ILogger<ReservationController> _logger;
    private readonly IRepository<ReservationDbEntity> _repository;
    private readonly IReservationApplicationService _reservationApplicationService;

    /// <summary>
    /// Initialize instance of object.
    /// </summary>
    public ReservationController(ILogger<ReservationController> logger,
        IRepository<ReservationDbEntity> repository, 
        IReservationApplicationService reservationApplicationService)
    {
        _logger = logger;
        _repository = repository;
        _reservationApplicationService = reservationApplicationService;
    }

    /// <summary>
    /// Get list active resource reservations.
    /// </summary>
    /// <returns>Active resource reservations falling into filter category.</returns>
    [HttpGet("{reservationId}")]
    public async Task<ActionResult<IEnumerable<ReservationDbEntity>>> Get([NotDefaultGuid] Guid reservationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Identifier must be non default");
        }

        return Ok(await _repository.Get(reservationId));
    }

    public async Task<ActionResult<IEnumerable<ReservationDbEntity>>> GetAll([FromQuery] GetReservationRequest request)
    {
        return Ok(await _repository.GetAll(request));
    }

    /// <summary>
    /// Creates reservations definition.
    /// </summary>
    /// <param name="request">Definition what have to be reserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpPost]
    public async Task<ActionResult<OperationResult<ReservationDbEntity>>> Create([FromBody] CreateReservationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("All identifiers must be provided and non default value.");
        }

        var createResult = await _reservationApplicationService.CreateResevation(new CreateReservationDto(default, default, default)
        {
            ReservationId = request.ReservationId,
            ReservingPartyId = request.ReservingPartyId,
            ResourceId = request.ResourceId
        });

        if(createResult.IsSuccess)
            return Ok(createResult);

        return BadRequest(createResult.Error);
    }

    /// <summary>
    /// Deletes reservations definition.
    /// </summary>
    /// <returns>Information about status of operation.</returns>
    [HttpDelete("{reservationId}")]
    public async Task<ActionResult> Delete([NotDefaultGuid] Guid reservationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reservation = await _repository.Get(reservationId);
        if (reservation == null)
        {
            return NotFound();
        }

        await _repository.Delete(reservation);
        return Ok();
    }
}
