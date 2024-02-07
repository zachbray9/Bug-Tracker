using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models.DTOs;

namespace BugTracker.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmPassword);
        Task<UserDTO> Login(string email, string password);
    }
}
