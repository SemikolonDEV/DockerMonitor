using Docker.DotNet;
using Docker.DotNet.Models;
using DockerMonitor.Domain;
using System.Runtime.InteropServices;

namespace DockerMonitor.Infastructure;

public class DockerInformation : IDockerInformation
{

    private readonly DockerClient dockerClient = new DockerClientConfiguration(LocalDockerUri()).CreateClient();

    public static Uri LocalDockerUri()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        return isWindows ? new Uri("npipe://./pipe/docker_engine"): new Uri("unix:///var/run/docker.sock");
    }

}