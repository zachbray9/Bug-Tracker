using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Services
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }
    public interface IUserService : IDataService<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
    }
}
