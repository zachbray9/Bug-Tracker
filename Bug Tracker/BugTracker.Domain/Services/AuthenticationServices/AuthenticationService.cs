using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Exceptions;
using BugTracker.Domain.Models.DTOs;
using BugTracker.Domain.Services.Api;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace BugTracker.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserApiService UserApiService;
        private readonly IPasswordHasher PasswordHasher;

        public AuthenticationService(IUserApiService userApiService, IPasswordHasher passwordHasher)
        {
            UserApiService = userApiService;
            PasswordHasher = passwordHasher;
        }

        public async Task<RegistrationResult> CreateAccount(string email, string firstName, string lastName, string password, string confirmPassword)
        {
            //RegistrationResult result = RegistrationResult.Success;

            //if any of the input fields are null
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
            {
                return RegistrationResult.InputFieldIsNull;
            }

            //if first or last name contains any specials characters
            if(!String.IsNullOrEmpty(firstName))
            {
                foreach (char c in firstName)
                {
                    if (!char.IsLetter(c))
                    {
                        return RegistrationResult.NameContainsSpecialCharacter;
                    }
                }
            }
            if(!String.IsNullOrEmpty(lastName))
            {
                foreach (char c in lastName)
                {
                    if (!char.IsLetter(c))
                    {
                        return RegistrationResult.NameContainsSpecialCharacter;
                    }
                }
            }

            //if email format is invalid
            if(!IsValidEmail(email))
            {
                return RegistrationResult.EmailFormatIsInvalid;
            }

            //if the passwords do not match
            if (password != confirmPassword)
            {
                return RegistrationResult.PasswordsDoNotMatch;
            }

            //if the email already exists for the account that is being created
            UserDTO userByEmail = await UserApiService.GetByEmail(email);
            if (userByEmail != null) 
            {
               return RegistrationResult.EmailAlreadyExists;
            }

            //if there are no errors then create the account
            string hashedPassword = PasswordHasher.HashPassword(password);

            UserDTO user = new UserDTO()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = hashedPassword,
                DateJoined = DateTime.Now
            };

            await UserApiService.Create(user);
        

            return RegistrationResult.Success;

        }

        public async Task<UserDTO> Login(string email, string password)
        {
            UserDTO? storedUser = await UserApiService.GetByEmail(email);

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

        private bool IsValidEmail(string email)
        {
            bool isValid = true;

            try
            {
                MailAddress mailAddress= new MailAddress(email);
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
