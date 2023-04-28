using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.State.Authenticators
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmpassword);
        Task<bool> Login(string email, string password);
        void Logout();

    }
}
