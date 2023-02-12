using BugTracker.Domain.Exceptions;
using BugTracker.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService UserService;
        private readonly IPasswordHasher PasswordHasher;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher)
        {
            UserService = userService;
            PasswordHasher = passwordHasher;
        }

        public async Task<RegistrationResult> CreateAccount(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            //if the passwords do not match
            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            //if the email already exists for the account that is being created
            User userByEmail = await UserService.GetByEmail(email);
            if (userByEmail != null) 
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            //if theh username already exists for the account that is being created
            User userByUsername = await UserService.GetByUsername(username);
            if (userByUsername != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            //if there are no errors then create the account
            if(result== RegistrationResult.Success) 
            {
            string hashedPassword = PasswordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DateJoined = DateTime.Now
                };

                await UserService.Create(user);
            }

            return result;

        }

        public async Task<User> Login(string username, string password)
        {
            User storedUser = await UserService.GetByUsername(username);

            if (storedUser == null)
            {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult = PasswordHasher.VerifyHashedPassword(storedUser.PasswordHash, password);

            if(passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedUser;
        }
    }
}
