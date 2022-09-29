using DockerMonitor.Domain.Entities;

namespace DockerMonitor.Domain.Interfaces;

public interface IDockerInformation
{

    public Task<IEnumerable<ContainerInfo>> GetAllContainer(CancellationToken cancellationToken = default);

    public Task<ContainerInfo> GetContainer(string id, CancellationToken cancellationToken = default);

    public void GetContainerStats(string id, Action<ContainerStat> act, CancellationToken cancellationToken = default);

    public Task<bool> StartContainer(string id, CancellationToken cancellationToken = default);

    public Task<bool> StopContainer(string id, CancellationToken cancellationToken = default);

    public Task RemoveContainer(string id, CancellationToken cancellationToken = default);
}