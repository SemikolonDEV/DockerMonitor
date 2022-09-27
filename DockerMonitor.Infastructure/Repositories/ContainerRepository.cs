using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DockerMonitor.Infastructure.Repositories;

public class ContainerRepository : IContainerRepository
{

    private readonly RepositoryContext _repositoryContext;

    public ContainerRepository(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public async Task Create(DBContainer dbContainer, CancellationToken cancellationToken = default)
    {
        await _repositoryContext.Containers.AddAsync(dbContainer, cancellationToken);
    }

    public async Task<DBContainer> GetDBContainer(string id, CancellationToken cancellationToken = default)
    {
        var container = await _repositoryContext.Containers.FirstOrDefaultAsync(container => container.DBContainerId == id, cancellationToken);
        return container;
    }
}
