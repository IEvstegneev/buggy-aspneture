namespace BuggyAspneture.DataAccess;

public class OpenLoopsRepository
{
    private static string _directoryName = "./openLoops/"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openLoops");

    static OpenLoopsRepository()
    {
        Directory.CreateDirectory(_directoryName);
    }

    public static OpenLoop[] Get()
    {
        var files = Directory.GetFiles(_directoryName);
        var openLoops = new List<OpenLoop>();
        foreach (var filePath in files)
        {
            var openLoop = JsonHelper.Read<OpenLoop>(filePath);
            openLoops.Add(openLoop);
        }
        return openLoops.ToArray();
    }

    public static string Add(OpenLoop openLoop)
    {
        var id = Guid.NewGuid();
        if (OpenLoopExists(id, out string filePath))
        {
            throw new InvalidOperationException($"File with name {filePath} exists already.");
        }
        else
        {
            var newOpenLoop = openLoop with { Id = id  };
            JsonHelper.Write(newOpenLoop, filePath);
            return _directoryName;
        }
    }

    /// <returns>Full path if deleting is successful and null if not.</returns>
    public static string Delete(Guid id)
    {
        if (OpenLoopExists(id, out string filePath))
        {
            File.Delete(filePath);
            return filePath;
        }
        else
        {
            return null;
        }
    }

    /// <returns>Full path if updating is successful and null if not.</returns>
    public static string Update(Guid id, string newText)
    {
        if (OpenLoopExists(id, out string filePath))
        {
            var openLoop = JsonHelper.Read<OpenLoop>(filePath);
            var updatedOpenLoops = openLoop with { Note = newText };
            JsonHelper.Write(updatedOpenLoops, filePath);
            return filePath;
        }
        else
        {
            return null;
        }
    }

    private static bool OpenLoopExists(Guid id, out string filePath)
    {
        filePath = Path.Combine(_directoryName, $"{id}.json");
        return File.Exists(filePath);
    }
}