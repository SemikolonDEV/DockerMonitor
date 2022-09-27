namespace DockerMonitor.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var one = 1.0055m;

        one = Math.Round(one, 2);

        one.Should().Be(1);
        
    }
}