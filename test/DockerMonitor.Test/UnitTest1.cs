namespace DockerMonitor.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var one = 1.0055m;
        one.Should().Be(1);
        Math.Round(one, 2);
    }
}