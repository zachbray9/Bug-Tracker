using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;
using System.Threading.Tasks;

namespace Bug_Tracker.State.Authenticators
{
    public interface IAuthenticator
    {
        UserDTO CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmpassword);
        Task<bool> Login(string email, string password);
        void Logout();

    }
}
