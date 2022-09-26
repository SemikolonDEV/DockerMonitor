using Docker.DotNet;
using Docker.DotNet.Models;
using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Exceptions;
using DockerMonitor.Domain.Interfaces;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DockerMonitor.Infastructure;

public class DockerInformation : IDockerInformation
{

    private readonly DockerClient _dockerClient = new DockerClientConfiguration(LocalDockerUri()).CreateClient();

    private static Uri LocalDockerUri()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        return isWindows ? new Uri("npipe://./pipe/docker_engine"): new Uri("unix:///var/run/docker.sock");
    }

    public async Task<IEnumerable<ContainerInfo>> GetAllContainer(CancellationToken cancellationToken = default)
    {
        var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = true }, cancellationToken);
        return containers.Select(containerResponse => new ContainerInfo 
        {
            Id = containerResponse.ID,
            Image = containerResponse.Image,
            State = containerResponse.State,
            Created = containerResponse.Created,
            Labels = containerResponse.Labels,
        });
    }

    public async Task<ContainerInfo> GetContainer(string id, CancellationToken cancellationToken = default)
    {
        var containerList = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All= true }, cancellationToken);
        var containerResponse = containerList.SingleOrDefault(c => c.ID == id);
        if (containerResponse == null)
        {
            throw new ContainerNotFoundException();
        }
        return new ContainerInfo
        {
            Id = containerResponse.ID,
            Image = containerResponse.Image,
            State = containerResponse.State,
            Created = containerResponse.Created,
            Labels = containerResponse.Labels,
        };
    }

    public async Task<ContainerStats> GetContainerStats(string id, CancellationToken cancellationToken = default)
    {
       var progress = new Progress<ContainerStatsResponse>(value =>
       {
           var networks = value.Networks.Select(d => d.Key);
       });
       await _dockerClient.Containers.GetContainerStatsAsync(id, new ContainerStatsParameters { }, progress, cancellationToken);
        return null;
    }

    public async Task<bool> StartContainer(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerClient.Containers.StartContainerAsync(id, new ContainerStartParameters { }, cancellationToken);
    }

    public async Task<bool> StopContainer(string id, CancellationToken cancellationToken  = default)
    {
        return await _dockerClient.Containers.StopContainerAsync(id, new ContainerStopParameters { } , cancellationToken);
    }
}