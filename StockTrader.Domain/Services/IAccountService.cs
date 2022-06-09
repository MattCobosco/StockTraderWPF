using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public interface IAccountService : IDataService<Account>
    {
        /// <summary>
        /// Get the account with the specified username.
        /// </summary>
        /// <param name="username">Username of the account.</param>
        /// <returns>The account with the username. Can be null.</returns>
        Task<Account> GetByUsername(string username);
        /// <summary>
        /// Get the account with the specified email.
        /// </summary>
        /// <param name="email">Email of the account.</param>
        /// <returns>The account with the specified email. Can be null.</returns>
        Task<Account> GetByEmail(string email);
    }
}
