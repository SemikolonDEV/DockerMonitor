namespace DockerMonitor.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var one = 1;
        one.Should().Be(1);
    }
}