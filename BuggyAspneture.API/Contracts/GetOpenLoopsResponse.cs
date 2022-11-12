namespace BuggyAspneture.API.Contracts
{
    public sealed class GetOpenLoopsResponse
    {
        public GetOpenLoopDto[] OpenLoops { get; set; }
    }

    public class GetOpenLoopDto
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}