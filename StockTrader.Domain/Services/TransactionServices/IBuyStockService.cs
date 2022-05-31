using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Buy a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="stockSymbol">The symbol of a stock bought.</param>
        /// <param name="shareAmount">The number of shares to buy.</param>
        /// <returns>The updated account after the buy transaction.</returns>
        /// <exception cref="InsufficientFundsException">Thrown if the account has insufficient balance.</exception>
        Task<Account> BuyStock(Account buyer, string stockSymbol, int shareAmount);
    }
}