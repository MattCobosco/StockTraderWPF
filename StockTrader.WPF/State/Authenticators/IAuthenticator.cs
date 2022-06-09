using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace StockTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }
        event Action StateChanged;

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
        /// Login to the app.
        /// </summary>
        /// <param name="username">User's name.</param>
        /// <param name="password">User's password.</param>
        /// <exception cref="UserNotFoundException">Thrown when username does not exist in the database.</exception>
        /// <exception cref="InvalidPasswordException">Thrown when the provided password is incorrect.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task Login(string username, string password);
        /// <summary>
        /// Logout of the app. UNUSED!
        /// </summary>
        void Logout();
    }
}
