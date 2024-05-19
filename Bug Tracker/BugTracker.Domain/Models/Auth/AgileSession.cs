namespace BugTracker.Domain.Models.Auth
{
    public class AgileSession
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
