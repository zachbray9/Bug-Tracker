using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmPassword);
        Task<User> Login(string email, string password);
    }
}
