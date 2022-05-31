using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="stockSymbol">The symbol of a stock sold.</param>
        /// <param name="shareAmount">The number of shares to sell.</param>
        /// <returns>The updated account after the sell transaction.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if the sold symbol is invalid.</exception>
        /// <exception cref="InsufficientSharesException">Thrown if the account does not have enough shares to sell.</exception>
        /// <exception cref="Exception">Thrown if the sell transaction fails.</exception>
        Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount);
    }
}
