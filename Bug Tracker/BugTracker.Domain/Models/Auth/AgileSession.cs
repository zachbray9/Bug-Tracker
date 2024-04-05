namespace BugTracker.Domain.Models.Auth
{
    public class AgileSession
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
