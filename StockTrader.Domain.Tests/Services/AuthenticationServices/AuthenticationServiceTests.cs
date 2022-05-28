using Microsoft.AspNet.Identity;
using Moq;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.Domain.Services.AuthenticationServices;

namespace StockTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public async Task Login_CorrectPasswordExistingUser_ReturnsAccountForCurrentUsername()
        {
            //ARRANGE
            string expectedUsername = "testUser";
            string password = "testPassword";
            Mock<IAccountService> mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(s=>s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            Mock<IPasswordHasher> mockPasswordHasher = new Mock<IPasswordHasher>();
            mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);
            AuthenticationService authenticationService = new AuthenticationService(mockAccountService.Object, mockPasswordHasher.Object);

            //ACT
            Account account = await authenticationService.Login(expectedUsername, password);

            //ASSERT
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }
    }
}
