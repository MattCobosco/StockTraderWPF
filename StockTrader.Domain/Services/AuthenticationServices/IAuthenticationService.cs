using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services
{
    // Enum for possible registration results.
    public enum RegistrationResult
    {
        Success,
        PasswordsNoMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
    }

    public interface IAuthenticationService
    {
        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <param name="email">Email of the new account. Must be unique.</param>
        /// <param name="username">Username of the new account. Must be unique.</param>
        /// <param name="password">Password of the new account. Must match confirmPassword.</param>
        /// <param name="confirmPassword">Password of the new account. Must match password.</param>
        /// <returns>One of the results defined in the RegistrationResult enum above.</returns>
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        /// <summary>
        /// Login to an existing account.
        /// </summary>
        /// <param name="username">Username of an existing account.</param>
        /// <param name="password">Password of an existing account.</param>
        /// <returns>An account that matches the qiven username and password.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the username is not in the database.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password does not match the username.</exception>
        Task<Account> Login(string username, string password);
    }
}