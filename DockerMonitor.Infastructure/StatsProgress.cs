using Docker.DotNet.Models;

namespace DockerMonitor.Infastructure;

public class StatsProgress : IProgress<ContainerStatsResponse>
{

    public void Report(ContainerStatsResponse value)
    {
        throw new NotImplementedException();
    }
}