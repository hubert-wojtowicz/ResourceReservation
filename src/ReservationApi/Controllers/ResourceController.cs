using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Application.Repository;

namespace ReservationApi.Controllers;

/// <summary>
/// REST controller over Resource.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private ILogger<ResourceController> _logger;

    /// <summary>
    /// Initialize instance of object.
    /// </summary>
    public ResourceController(
        ILogger<ResourceController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get list of resources.
    /// </summary>
    [HttpGet]
    public Task<IEnumerable<object>> ListActiveReservations([FromQuery]GetResourceRequest request)
    {
        return Task.FromResult<IEnumerable<object>>(null);
    }


    /// <summary>
    /// Creates resourcw.
    /// </summary>
    [HttpPost]
    public Task Create([FromBody]CreateResourceRequest request)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Removes resource.
    /// </summary>
    [HttpDelete]
    public Task Delete([FromBody] DeleteResourceRequest request)
    {
        return Task.CompletedTask;
    }
}
