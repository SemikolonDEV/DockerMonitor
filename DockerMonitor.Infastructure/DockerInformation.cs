using Docker.DotNet;
using Docker.DotNet.Models;
using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Exceptions;
using DockerMonitor.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace DockerMonitor.Infastructure;

public class DockerInformation : IDockerInformation
{

    private readonly DockerClient _dockerClient = new DockerClientConfiguration(LocalDockerUri()).CreateClient();

    private static Uri LocalDockerUri()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:///var/run/docker.sock");
    }

    public async Task<IEnumerable<ContainerInfo>> GetAllContainer(CancellationToken cancellationToken = default)
    {
        var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = true }, cancellationToken);
        return containers.Select(containerResponse => new ContainerInfo
        {
            Id = containerResponse.ID,
            Names = containerResponse.Names,
            Image = containerResponse.Image,
            State = containerResponse.State,
            Created = containerResponse.Created,
            Labels = containerResponse.Labels,
            Volumes = containerResponse.Mounts.Select(d  => $"{d.Source}:{d.Destination}")
        });
    }

    public async Task<ContainerInfo> GetContainer(string id, CancellationToken cancellationToken = default)
    {
        var containerList = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters { All = true }, cancellationToken);
        var containerResponse = containerList.SingleOrDefault(c => c.ID == id);
        if (containerResponse is null)
        {
            throw new ContainerNotFoundException();
        }
        return new ContainerInfo
        {
            Id = containerResponse.ID,
            Names = containerResponse.Names,
            Image = containerResponse.Image,
            State = containerResponse.State,
            Created = containerResponse.Created,
            Labels = containerResponse.Labels,
            Volumes = containerResponse.Mounts.Select(d => $"{d.Source}:{d.Destination}")
        };
    }

    public async void GetContainerStats(string id, Action<ContainerStat> act, CancellationToken cancellationToken = default)
    {
        var progress = new Progress<ContainerStatsResponse>(value =>
        {
            var containerStats = new ContainerStat
            {
                DBContainerId = value.ID,
                TimeStamp = value.Read,
                CPUUsage = value.CPUStats.CPUUsage.TotalUsage,
                Cores = value.CPUStats.OnlineCPUs,
                MemoryUsage = value.MemoryStats.Usage,
                MemoryMax = value.MemoryStats.Limit,
                ReadSize = value.StorageStats.ReadSizeBytes,
                WriteSize = value.StorageStats.WriteSizeBytes
            };
            act(containerStats);
        });
        await _dockerClient.Containers.GetContainerStatsAsync(id, new ContainerStatsParameters { OneShot = true, Stream = false }, progress, cancellationToken);
    }

    public async Task<bool> StartContainer(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerClient.Containers.StartContainerAsync(id, new ContainerStartParameters { }, cancellationToken);
    }

    public async Task<bool> StopContainer(string id, CancellationToken cancellationToken = default)
    {
        return await _dockerClient.Containers.StopContainerAsync(id, new ContainerStopParameters { }, cancellationToken);
    }
}