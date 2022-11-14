using System.Text.Json;

namespace BuggyAspneture.DataAccess
{
    public static class JsonHelper
    {
        public static OpenLoop Read(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var openLoop = JsonSerializer.Deserialize<OpenLoop>(json);
            if (openLoop == null)
            {
                throw new NullReferenceException("OpenLoop cannot be deserialized.");
            }
            return openLoop;
        }

        public static void Write(OpenLoop openLoop, string filePath)
        {
            if (openLoop == null)
            {
                throw new NullReferenceException("OpenLoop cannot be serialized.");
            }
            var json = JsonSerializer.Serialize(openLoop, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}