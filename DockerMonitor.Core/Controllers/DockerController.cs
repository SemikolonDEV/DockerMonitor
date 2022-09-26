using DockerMonitor.Domain.Entities;
using DockerMonitor.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DockerMonitor.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DockerController : ControllerBase
{

    private readonly IDockerService _dockerService;

    public DockerController(IDockerService dockerService)
    {
        _dockerService = dockerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContainerInfo>>> GetAllContainer(CancellationToken cancellationToken = default)
    {
        var conatinerList = await _dockerService.GetAllContainer(cancellationToken);
        return Ok(conatinerList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContainerInfo>> GetContainerInfo(string id, CancellationToken cancellationToken = default)
    {
        var conatiner = await _dockerService.GetContainerInfo(id, cancellationToken);
        return Ok(conatiner);
    }

    [HttpGet("start/{id}")]
    public async Task<ActionResult<bool>> StartContainer(string id, CancellationToken cancellationToken = default)
    {
        var isSucess = await _dockerService.StartContainer(id, cancellationToken);
        return Ok(isSucess);
    }

    [HttpGet("stop/{id}")]
    public async Task<ActionResult<bool>> StopContainer(string id, CancellationToken cancellationToken = default)
    {
        var isSucess = await _dockerService.StopContainer(id, cancellationToken);
        return Ok(isSucess);
    }

}
