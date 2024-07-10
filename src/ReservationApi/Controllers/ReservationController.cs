using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Domain;

namespace ReservationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(ILogger<ReservationController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// List resources.
    /// </summary>
    /// <returns>Availiable resources.</returns>
    [HttpGet]
    public Task<IEnumerable<Reservation>> Get()
    {
        return Task.FromResult<IEnumerable<Reservation>>(null);
    }

    /// <summary>
    /// Creates resource definition.
    /// </summary>
    /// <param name="request">Definition what have to be reserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpPost]
    public Task Create([FromBody] CreateReservationRequest request)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes resource definition.
    /// </summary>
    /// <param name="request">Definition what have to be unreserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpDelete]
    public Task Delete([FromBody] DeleteReservationRequest request)
    {
        return Task.CompletedTask;
    }
}
