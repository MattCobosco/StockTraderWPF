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
        void Logout();
    }
}
