using Microsoft.AspNet.Identity;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountService.GetByUsername(username);

            if (storedAccount == null)
                throw new UserNotFoundException(username);

            PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            if(passwordVerificationResult != PasswordVerificationResult.Success)
                throw new InvalidPasswordException(username, password);

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return RegistrationResult.PasswordsNoMatch;

            Account accountByEmail = await _accountService.GetByEmail(email);
            if (accountByEmail != null)
                return RegistrationResult.EmailAlreadyExists;

            Account accountByUsername = await _accountService.GetByUsername(username);
            if (accountByUsername != null)
                return RegistrationResult.UsernameAlreadyExists;

            string hashedPassword = _passwordHasher.HashPassword(password);

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

            return RegistrationResult.Success;
        }
    }
}