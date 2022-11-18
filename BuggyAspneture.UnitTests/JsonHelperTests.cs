using BuggyAspneture.DataAccess;
using System.Security.Cryptography.X509Certificates;

namespace BuggyAspneture.UnitTests;

public class JsonHelperTests
{
    [Fact]
    public void FailOnWriteNull()
    {
        var write = () => JsonHelper.Write(null, @".\test.json");

        Assert.Throws<NullReferenceException>(write);
    }

    [Fact]
    public void Write_FileShouldBeCreated()
    {
        var path = @".\test.json";
        JsonHelper.Write(new OpenLoop(), path);
        var result = File.Exists(path);
        if (result) File.Delete(path);
        Assert.True(result);
    }

    [Fact]
    public void ReadWrite_ShouldWork()
    {
        var path = @".\test.json";
        var expected = new OpenLoop();
        JsonHelper.Write(expected, path);
        var actual = JsonHelper.Read(path);
        File.Delete(path); 
        Assert.Equal(expected, actual);
    }
}
