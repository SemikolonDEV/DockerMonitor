using DockerMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DockerMonitor.Infastructure;

public class RepositoryContext : DbContext
{

    public DbSet<DBContainer> Containers { get; set; }
    public DbSet<ContainerStat> ContainersStats { get; set; }

    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

}
