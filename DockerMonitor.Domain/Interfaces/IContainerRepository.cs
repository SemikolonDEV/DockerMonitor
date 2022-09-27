using DockerMonitor.Domain.Entities;

namespace DockerMonitor.Domain.Interfaces;

public interface IContainerRepository
{

    public Task Create(DBContainer dBContainer, CancellationToken cancellationToken = default);

    public Task<DBContainer> GetDBContainer(string id, CancellationToken cancellationToken = default);

}

