using BuggyAspneture.DataAccess;

namespace BuggyAspneture.UnitTests;

public class OpenLoopRepositoryTests
{
    [Fact]
    public void FailOnWriteNull()
    {
        var write = () => JsonHelper.Write(null, @".\test.json");

        Assert.Throws<NullReferenceException>(write);
    }
}
