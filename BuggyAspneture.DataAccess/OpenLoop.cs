using System.Reflection.Metadata.Ecma335;

namespace BuggyAspneture.DataAccess;

public record OpenLoop
{
    public Guid Id { get; init; }
    public string Note { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}