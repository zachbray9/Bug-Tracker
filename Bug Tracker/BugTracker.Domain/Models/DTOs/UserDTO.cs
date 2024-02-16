namespace BugTracker.Domain.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get => FirstName + " " + LastName; }
        public string Initials { get => $"{FirstName?.FirstOrDefault()}{LastName?.FirstOrDefault()}".ToUpper(); }
        public string PasswordHash { get; set; } = null!;
        public DateTime DateJoined { get; set; }

        public override string ToString()
        {
            return Email;
        }
    }
}
