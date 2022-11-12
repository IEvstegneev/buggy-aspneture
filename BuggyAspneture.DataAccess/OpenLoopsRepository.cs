using System.Text.Json;

namespace BuggyAspneture.DataAccess;

public class OpenLoopsRepository
{
    private static string _directoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openLoops");

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
            var openLoop = Read(filePath);
            openLoops.Add(openLoop);
        }
        return openLoops.ToArray();
    }

    public static Guid Add(OpenLoop openLoop)
    {
        var newOpenLoop = openLoop with { Id = Guid.NewGuid() };
        Write(newOpenLoop);
        return newOpenLoop.Id;
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
            var openLoop = Read(filePath);
            var updateOpenLoops = openLoop with { Note = newText };
            Write(updateOpenLoops);
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

    private static OpenLoop Read(string file)
    {
        var json = File.ReadAllText(file);
        var openLoop = JsonSerializer.Deserialize<OpenLoop>(json);
        if (openLoop == null)
        {
            throw new NullReferenceException("OpenLoop cannot be deserialized.");
        }
        return openLoop;
    }

    private static void Write(OpenLoop openLoop)
    {
        if (openLoop == null)
        {
            throw new NullReferenceException("OpenLoop cannot be serialized.");
        }
        var json = JsonSerializer.Serialize(openLoop,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        var fileName = $"{openLoop.Id}.json";
        var filePath = Path.Combine(_directoryName, fileName);
        File.WriteAllText(filePath, json);
    }
}