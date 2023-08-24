using Moq;
using BugTracker.Domain.Services;
using BugTracker.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BugTracker.Domain.Models;
using BugTracker.Domain.Exceptions;

namespace BugTracker.Domain.UnitTests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        private AuthenticationService authenticationService;
        private Mock<IUserService> mockUserService;
        private Mock<IPasswordHasher> mockPasswordHasher;

        [SetUp]
        public void Setup()
        {
            mockUserService = new Mock<IUserService>();
            mockPasswordHasher = new Mock<IPasswordHasher>();
            authenticationService = new AuthenticationService(mockUserService.Object, mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUser_ReturnsUserforCorrectUsername()
        {
            //arrange
            string expectedEmail = "test@gmail.com";
            string password = "testPassword";
            mockUserService.Setup(s => s.GetByEmail(expectedEmail)).ReturnsAsync(new User() { Email = expectedEmail});
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            //act
            User user =  await authenticationService.Login(expectedEmail, password);

            //assert
            string actualEmail = user.Email;
            Assert.AreEqual(expectedEmail, actualEmail);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingEmail_ThrowsInvalidExceptionForUsername()
        {
            //arrange
            string expectedEmail = "test@gmail.com";
            string password = "testPassword";
            //string incorrectPassword = "asdfasdfasdf";
            mockUserService.Setup(s => s.GetByEmail(expectedEmail)).ReturnsAsync(new User() { Email = expectedEmail });
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => authenticationService.Login(expectedEmail, password));

            //assert
            string actualEmail = exception.Email;
            Assert.AreEqual(expectedEmail, actualEmail);
        }

        [Test]
        public void Login_WithNonExistingEmail_ThrowsInvalidExceptionForUsername()
        {
            //arrange
            string expectedEmail = "test@gmail.com";
            string password = "testPassword";
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => authenticationService.Login(expectedEmail, password));

            //assert
            string actualEmail = exception.Email;
            Assert.AreEqual(expectedEmail, actualEmail);
        }

        [Test]
        public async Task CreateAccount_PasswordsDoNotMatch_ReturnsPasswordsDoNotMatchRegistrationResult()
        {
            string password = "testPassword";
            string confirmPassword = "incorrectTestPassword";
            RegistrationResult expectedResult = RegistrationResult.PasswordsDoNotMatch;

            RegistrationResult result = await authenticationService.CreateAccount("Nonexistingemail@nonexistingemail.com", "TestFirstName", "TestLastName", password, confirmPassword);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateAccount_EmailAlreadyExists_ReturnsEmailAlreadyExistsRegistrationResult()
        {
            string email = "test@gmail.com";
            RegistrationResult expectedResult = RegistrationResult.EmailAlreadyExists;

            mockUserService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new User());
            RegistrationResult result =  await authenticationService.CreateAccount(email, "TestFirstName", "TestLastName", "test", "test");

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateAccount_WithNonExistingEmailAndMatchingPasswords_ReturnsSuccessRegistrationResult()
        {
            RegistrationResult expectedResult = RegistrationResult.Success;

            //mockUserService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new User());
            RegistrationResult result = await authenticationService.CreateAccount("nonexistingemail@nonexistingemail.com", "TestFirstName", "TestLastName", "test", "test");

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateAccount_WithFirstNameContainingSpecialCharacters_ReturnsNameContainsSpecialCharacterRegistrationResult()
        {
            string firstName = "Test@";
            RegistrationResult expectedResult = RegistrationResult.NameContainsSpecialCharacter;

            RegistrationResult result = await authenticationService.CreateAccount("nonexistingemail@nonexistingemail.com", firstName, "TestLastName", "test", "test");
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateAccount_WithLastNameContainingSpecialCharacters_ReturnsNameContainsSpecialCharacterRegistrationResult()
        {
            string lastName = "Test@";
            RegistrationResult expectedResult = RegistrationResult.NameContainsSpecialCharacter;

            RegistrationResult result = await authenticationService.CreateAccount("nonexistingemail@nonexistingemail.com", "TestFirstName", lastName, "test", "test");
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateAccount_WithEmailFormatBeingInvalid_ReturnsEmailFormatIsInvalidRegistrationResult()
        {
            string email = "test@@gmail.com";
            RegistrationResult expectedResult = RegistrationResult.EmailFormatIsInvalid;

            RegistrationResult result = await authenticationService.CreateAccount(email, "TestFirstName", "TestLastName", "test", "test");
            Assert.AreEqual(expectedResult, result);
        }
    }
}
