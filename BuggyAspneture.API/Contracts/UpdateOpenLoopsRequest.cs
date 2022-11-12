namespace BuggyAspneture.API.Contracts
{
    public sealed class UpdateOpenLoopsRequest
    {
        public string Id { get; set; }
        public string NewText { get; set; }
    }
}