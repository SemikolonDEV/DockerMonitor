using DockerMonitor.Domain;
using DockerMonitor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerMonitor.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DockerController : ControllerBase
{

    private readonly DockerService _dockerService;

    public DockerController(DockerService dockerService)
    {
        _dockerService = dockerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContainerInfo>>> GetAllContainer(CancellationToken cancellationToken = default)
    {
        var conatinerList = await _dockerService.GetAllContainer(cancellationToken);
        return Ok(conatinerList);
    }

    [HttpGet("{id:string}")]
    public async Task<ActionResult<ContainerInfo>> GetContainerInfo(string id, CancellationToken cancellationToken = default)
    {
        var conatiner = await _dockerService.GetContainerInfo(id, cancellationToken);
        return Ok(conatiner);
    }



}
