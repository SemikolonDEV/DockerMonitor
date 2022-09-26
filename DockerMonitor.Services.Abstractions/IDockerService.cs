using DockerMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerMonitor.Services.Abstractions;

public interface IDockerService
{

    public Task<IEnumerable<ContainerInfo>> GetAllContainer(CancellationToken cancellationToken = default);

    public Task<ContainerInfo> GetContainerInfo(string id, CancellationToken cancellationToken = default);

    public Task<bool> StartContainer(string id, CancellationToken cancellationToken = default);

    public Task<bool> StopContainer(string id, CancellationToken cancellationToken = default);


}
