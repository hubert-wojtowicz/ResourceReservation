using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;

namespace ReservationApi.Controllers;

/// <summary>
/// Manages exclusive reservation over information defined with <seealso cref="ResourceController"/>.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private ILogger<ResourceController> _logger;

    public ResourceController(ILogger<ResourceController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get list active resource reservations.
    /// </summary>
    /// <returns>Active resource reservations falling into filter category.</returns>
    [HttpGet]
    public Task<IEnumerable<object>> ListActiveReservations()
    {
        return Task.FromResult<IEnumerable<object>>(null);
    }


    /// <summary>
    /// Creates reservation of resource.
    /// </summary>
    /// <param name="request">Definition what have to be reserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpPost]
    public Task Create(CreateReservationRequest request)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Removes reservation of resource.
    /// </summary>
    /// <param name="request">Definition what have to be unreserved.</param>
    /// <returns>Information about status of operation.</returns>
    [HttpDelete]
    public Task Delete([FromBody] DeleteReservationRequest request)
    {
        return Task.CompletedTask;
    }
}
