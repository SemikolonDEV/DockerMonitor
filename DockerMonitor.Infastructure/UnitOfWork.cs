using DockerMonitor.Domain.Interfaces;

namespace DockerMonitor.Infastructure;

public class UnitOfWork : IUnitOfWork
{

    private readonly RepositoryContext _repositoryContext;

    public UnitOfWork(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _repositoryContext.SaveChangesAsync(cancellationToken);
    }
}
