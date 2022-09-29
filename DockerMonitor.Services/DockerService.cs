using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Interfaces;
using DockerMonitor.Services.Abstractions;

namespace DockerMonitor.Services;

public class DockerService : IDockerService
{

    private readonly IDockerInformation _dockerInformation;

    public DockerService(IDockerInformation dockerInformation)
    {
        _dockerInformation = dockerInformation;
    }

    public async Task<IEnumerable<ContainerInfo>> GetAllContainer(CancellationToken cancellationToken = default)
    {
        return await _dockerInformation.GetAllContainer(cancellationToken);
    }

    public async Task<ContainerInfo> GetContainerInfo(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerInformation.GetContainer(id, cancellationToken);
    }

    public async Task<bool> RemoveContainer(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dockerInformation.RemoveContainer(id, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> StartContainer(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerInformation.StartContainer(id, cancellationToken);
    }

    public async Task<bool> StopContainer(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerInformation.StopContainer(id, cancellationToken);
    }
}