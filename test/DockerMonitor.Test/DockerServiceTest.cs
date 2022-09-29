using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Interfaces;
using DockerMonitor.Services;
using Moq;

namespace DockerMonitor.Test;

public class DockerServiceTest
{

    private readonly Mock<IDockerInformation> _dockerInformationMock = new Mock<IDockerInformation>();

    [Fact]
    public async void GetAllContainer_IsTypeListContainerInfo()
    {
        //Arrange
        var dockerService = new DockerService(_dockerInformationMock.Object);
        //Act
        var result = await dockerService.GetAllContainer();
        //Assert
        result.Should().AllBeOfType<ContainerInfo>();
        result.Count().Should().Be(0);
    }
}