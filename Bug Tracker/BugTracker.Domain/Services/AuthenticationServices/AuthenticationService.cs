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

        public async Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmPassword)
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

            //if there are no errors then create the account
            if(result== RegistrationResult.Success) 
            {
            string hashedPassword = PasswordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PasswordHash = hashedPassword,
                    DateJoined = DateTime.Now
                };

                await UserService.Create(user);
            }

            return result;

        }

        public async Task<User> Login(string email, string password)
        {
            User storedUser = await UserService.GetByEmail(email);

            if (storedUser == null)
            {
                throw new UserNotFoundException(email);
            }

            PasswordVerificationResult passwordResult = PasswordHasher.VerifyHashedPassword(storedUser.PasswordHash, password);

            if(passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(email, password);
            }

            return storedUser;
        }
    }
}
