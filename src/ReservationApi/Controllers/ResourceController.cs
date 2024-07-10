using Microsoft.AspNetCore.Mvc;
using ReservationApi.ApiContract;
using ReservationApi.Application.Repository;
using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Controllers;

/// <summary>
/// REST controller over Resource.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ResourceController : ControllerBase
{
    private ILogger<ResourceController> _logger;
    private readonly IRepository<Resource> _repository;

    /// <summary>
    /// Initialize instance of object.
    /// </summary>
    public ResourceController(
        ILogger<ResourceController> logger, 
        IRepository<ReservationApi.Infrastructure.Entities.Resource> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// Get list of resources.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resource>>> Get([FromQuery]GetResourceRequest request)
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

        await _repository.Add(new Resource
        {
            Tags = request.Tags,
            ResourceId = request.ResourceId
        });

        return Ok();
    }

    /// <summary>
    /// Removes resource.
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteResourceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resource = await _repository.Get(request.ResourceId);

        if (resource == null)
        { 
            return NotFound();
        }

        await _repository.Delete(resource);

        return Ok();
    }
}
