using Microsoft.AspNet.Identity;
using Moq;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.Domain.Services.AuthenticationServices;

namespace StockTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockAccountService = new Mock<IAccountService>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            // ARRANGE
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            // ACT
            Account account = await _authenticationService.Login(expectedUsername, password);

            /// ASSERT
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // ARRANGE
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // ACT
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            // ASSERT
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // ARRANGE
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // ACT
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            // ASSERT
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            // ARRANGE
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";
            RegistrationResult expected = RegistrationResult.PasswordsNoMatch;

            // ACT
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            // ARRANGE
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            // ACT
            RegistrationResult actual = await _authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            // ARRANGE
            string username = "testuser";
            _mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            // ACT
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            // ARRANGE
            RegistrationResult expected = RegistrationResult.Success;

            // ACT
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // ASSERT
            Assert.AreEqual(expected, actual);
        }
    }
}
