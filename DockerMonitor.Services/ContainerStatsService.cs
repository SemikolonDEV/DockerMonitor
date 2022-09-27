using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DockerMonitor.Services;

public class ContainerStatsService : BackgroundService, IDisposable
{
    private readonly IServiceProvider _services;

    private bool isRunning;

    public ContainerStatsService(IServiceProvider services)
    {
        _services = services;
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {

        while (isRunning)
        {
            await Task.Delay(20000);
            var scope = _services.CreateScope();
            var dockerInformation = scope.ServiceProvider.GetRequiredService<IDockerInformation>();
            var container = await dockerInformation.GetAllContainer();
            foreach (var item in container)
            {
                var localScope = _services.CreateScope();
                var containerRepository = localScope.ServiceProvider.GetRequiredService<IContainerRepository>();
                var unitOfWork = localScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                dockerInformation.GetContainerStats(item.Id, async stats =>
                {
                    var dbContainer = await containerRepository.GetDBContainer(item.Id);

                    if (dbContainer == null)
                    {
                        dbContainer = new DBContainer
                        {
                            DBContainerId = item.Id,
                        };
                        await containerRepository.Create(dbContainer);
                    }
                    dbContainer.Stats.Add(stats);
                    await unitOfWork.SaveChangesAsync();
                });
            }
            
        }

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        isRunning = true;
        await DoWork(stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        isRunning = false;
        return base.StopAsync(cancellationToken);
    }
}
