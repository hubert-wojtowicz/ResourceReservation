using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Domain;

namespace ReservationApi.Controllers;

/// <summary>
/// REST controller over Reservation.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ILogger<ReservationController> _logger;

    /// <summary>
    /// Initialize instance of object.
    /// </summary>
    public ReservationController(ILogger<ReservationController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get list active resource reservations.
    /// </summary>
    /// <returns>Active resource reservations falling into filter category.</returns>
    [HttpGet]
    public Task<IEnumerable<Reservation>> Get(GetReservationRequest request)
    {
        return Task.FromResult<IEnumerable<Reservation>>(null);
    }

    /// <summary>
    /// Creates reservations definition.
    /// </summary>
    /// <param name="request">Definition what have to be reserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpPost]
    public Task Create([FromBody] CreateReservationRequest request)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes reservations definition.
    /// </summary>
    /// <param name="request">Definition what have to be unreserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpDelete]
    public Task Delete([FromBody] DeleteReservationRequest request)
    {
        return Task.CompletedTask;
    }
}
