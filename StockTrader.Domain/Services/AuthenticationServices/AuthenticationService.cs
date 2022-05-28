using Microsoft.AspNet.Identity;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataService<Account> _accountService;

        public AuthenticationService(IDataService<Account> accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return false;

            IPasswordHasher hasher = new PasswordHasher();
            string hashedPassword = hasher.HashPassword(password);

            User user = new User()
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
                DateJoined = DateTime.Now
            };

            Account account = new Account()
            {
                AccountHolder = user
            };

            await _accountService.Create(account);

            return true;
        }
    }
}