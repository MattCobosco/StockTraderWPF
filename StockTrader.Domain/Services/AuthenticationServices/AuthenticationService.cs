using Microsoft.AspNet.Identity;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.AuthenticationServices
{
    /// <summary>
    /// Implements IAuthenticationService.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Login to an existing account.
        /// </summary>
        /// <param name="username">Username of an existing account.</param>
        /// <param name="password">Password of an existing account.</param>
        /// <returns>An account that matches the qiven username and password.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the username is not in the database.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password does not match the username.</exception>
        public async Task<Account> Login(string username, string password)
        {
            // Get the account from the database by username using AccountService.
            Account storedAccount = await _accountService.GetByUsername(username);

            // If the account is null, throw UserNotFoundException.
            if (storedAccount == null)
                throw new UserNotFoundException(username);

            // Check if the provided password matches the stored password using PasswordHasher.
            PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.PasswordHash, password);

            // If the password does not match, throw InvalidPasswordException.
            if (passwordVerificationResult != PasswordVerificationResult.Success)
                throw new InvalidPasswordException(username, password);
            
            // If the password matches, return the account.
            return storedAccount;
        }

        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <param name="email">Email of the new account. Must be unique.</param>
        /// <param name="username">Username of the new account. Must be unique.</param>
        /// <param name="password">Password of the new account. Must match confirmPassword.</param>
        /// <param name="confirmPassword">Password of the new account. Must match password.</param>
        /// <returns>One of the results defined in the RegistrationResult enum above.</returns>
        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            // If passwords do not match, return RegistrationResult.PasswordsDoNotMatch.
            if (password != confirmPassword)
                return RegistrationResult.PasswordsNoMatch;

            // Check if the email is already in the database using AccountService.
            Account accountByEmail = await _accountService.GetByEmail(email);
            // If the email is in the database, return EmailAlreadyExists.
            if (accountByEmail != null)
                return RegistrationResult.EmailAlreadyExists;

            // Check if the username is already in the database using AccountService.
            Account accountByUsername = await _accountService.GetByUsername(username);
            // If the username is in the database, return UsernameAlreadyExists.
            if (accountByUsername != null)
                return RegistrationResult.UsernameAlreadyExists;

            // Hash the password using PasswordHasher.
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Create a new user account using the provided email, username, and hashed password, set DateJoined to current date and time.
            User user = new User()
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
                DateJoined = DateTime.Now
            };

            // Create a new account using the provided user.
            Account account = new Account()
            {
                AccountHolder = user
            };

            // Add the account to the database using AccountService.
            await _accountService.Create(account);

            // Return Success.
            return RegistrationResult.Success;
        }
    }
}