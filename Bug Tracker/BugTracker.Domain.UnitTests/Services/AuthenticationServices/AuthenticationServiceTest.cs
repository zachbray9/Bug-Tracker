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
            string expectedUsername = "testUsername";
            string password = "testPassword";
            mockUserService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new User() { Username = expectedUsername});
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            //act
            User user =  await authenticationService.Login(expectedUsername, password);

            //assert
            string actualUsername = user.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidExceptionForUsername()
        {
            //arrange
            string expectedUsername = "testUsername";
            string password = "testPassword";
            //string incorrectPassword = "asdfasdfasdf";
            mockUserService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new User() { Username = expectedUsername });
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => authenticationService.Login(expectedUsername, password));

            //assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidExceptionForUsername()
        {
            //arrange
            string expectedUsername = "testUsername";
            string password = "testPassword";
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => authenticationService.Login(expectedUsername, password));

            //assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task CreateAccount_PasswordsDoNotMatch_ReturnsPasswordsDoNotMatchRegistrationResult()
        {
            string password = "testPassword";
            string confirmPassword = "incorrectTestPassword";
            RegistrationResult expectedResult = RegistrationResult.PasswordsDoNotMatch;

            RegistrationResult result = await authenticationService.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public async Task CreateAccount_EmailAlreadyExists_ReturnsEmailAlreadyExistsRegistrationResult()
        {
            string email = "test@gmail.com";
            RegistrationResult expectedResult = RegistrationResult.EmailAlreadyExists;

            mockUserService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new User());
            RegistrationResult result =  await authenticationService.CreateAccount(email, It.IsAny<string>(), "test", "test");

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public async Task CreateAccount_UsernameAlreadyExists_ReturnsUsernameAlreadyExistsRegistrationResult()
        {
            string username = "testUsername";
            RegistrationResult expectedResult = RegistrationResult.UsernameAlreadyExists;

            mockUserService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new User());
            RegistrationResult result = await authenticationService.CreateAccount(It.IsAny<string>(), username, "test", "test");

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public async Task CreateAccount_WithNonExistingUsernameAndMatchingPasswords_ReturnsSuccessRegistrationResult()
        {
            RegistrationResult expectedResult = RegistrationResult.Success;

            //mockUserService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new User());
            RegistrationResult result = await authenticationService.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), "test", "test");

            Assert.AreEqual(result, expectedResult);
        }
    }
}
