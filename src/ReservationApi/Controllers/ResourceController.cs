using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure.Entities;
using ReservationApi.Validations;

namespace ReservationApi.Controllers;

/// <summary>
/// REST controller over Resource.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private ILogger<ResourceController> _logger;
    private readonly IRepository<ResourceDbEntity> _repository;

    /// <summary>
    /// Initialize instance of object.
    /// </summary>
    public ResourceController(
        ILogger<ResourceController> logger, 
        IRepository<ReservationApi.Infrastructure.Entities.ResourceDbEntity> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// Get list of resources.
    /// </summary>
    [ResponseCache(Duration = 10)] // just as example, another option is to inject IMemoryCache via constructor or configure Redis or other cache pervider behind IDistributedCache
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceDbEntity>>> Get([FromQuery]GetResourceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await _repository.GetAll(request));
    }

    /// <summary>
    /// Creates resource.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody]CreateResourceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _repository.Add(new ResourceDbEntity
        {
            Tags = request.Tags,
            ResourceId = request.ResourceId
        });

        return Ok();
    }

    /// <summary>
    /// Removes resource.
    /// </summary>
    [HttpDelete("{resourceId}")]
    public async Task<ActionResult> Delete([NotDefaultGuid] Guid resourceId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resource = await _repository.Get(resourceId);

        if (resource == null)
        { 
            return NotFound();
        }

        await _repository.Delete(resource);
        return Ok();
    }
}
