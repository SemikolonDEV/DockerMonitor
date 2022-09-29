using DockerMonitor.Domain.Entities;
using DockerMonitor.Domain.Exceptions;
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

    [Fact]
    public async void GetContainerInfo_ReturnsValidContainer()
    {
        //Arrange

        var id = "abc";
        CancellationToken cancellationToken = default;
        _dockerInformationMock.Setup(info => info.GetContainer("abc", cancellationToken)).ReturnsAsync(new ContainerInfo { Id = id });
        var dockerService = new DockerService(_dockerInformationMock.Object);
        //Act
        var result = await dockerService.GetContainerInfo(id);
        //Assert
        result.Should().BeOfType<ContainerInfo>();
        result.Id.Should().Be(id);
    }

    [Fact]
    public async void GetContainerInfo_ThrowContainerNotFoundException()
    {
        //Arrange

        var id = "abc";
        CancellationToken cancellationToken = default;
        _dockerInformationMock.Setup(info => info.GetContainer("abc", cancellationToken)).ThrowsAsync(new ContainerNotFoundException());
        var dockerService = new DockerService(_dockerInformationMock.Object);
        //Act
        var exec = () => dockerService.GetContainerInfo(id);
        //Assert
        await exec.Should().ThrowExactlyAsync<ContainerNotFoundException>();
    }

    [Fact]
    public async void StartContainer_ReturnFalse()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        _dockerInformationMock.Setup(info => info.StartContainer("abc", cancellationToken)).ReturnsAsync(false);
        var dockerService = new DockerService(_dockerInformationMock.Object);
        //Act
        var result = await dockerService.StartContainer("abc");
        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async void StartContainer_ReturnTrue()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        _dockerInformationMock.Setup(info => info.StartContainer("abc", cancellationToken)).ReturnsAsync(true);
        var dockerService = new DockerService(_dockerInformationMock.Object);
        //Act
        var result = await dockerService.StartContainer("abc");
        //Assert
        result.Should().BeTrue();
    }
}