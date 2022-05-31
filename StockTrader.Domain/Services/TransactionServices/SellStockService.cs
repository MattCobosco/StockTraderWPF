using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="stockSymbol">The symbol of a stock sold.</param>
        /// <param name="shareAmount">The number of shares to sell.</param>
        /// <returns>The updated account after the sell transaction.</returns>
        public Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount)
        {
            throw new NotImplementedException();
        }
    }
}
