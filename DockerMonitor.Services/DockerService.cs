using DockerMonitor.Domain;
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

    public Task<ContainerInfo> GetContainerInfo(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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