using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services.TransactionServices
{
    /// <summary>
    /// Implements ISellStockService.
    /// </summary>
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        /// <summary>
        /// Method to sell stock.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="stockSymbol">The symbol of a stock sold.</param>
        /// <param name="shareAmount">The number of shares to sell.</param>
        /// <returns>The updated account after the sell transaction.</returns>
        /// <exception cref="InsufficientSharesException">Thrown if the account does not have enough shares to sell.</exception>
        public async Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount)
        {
            // Get the total amount of shares of a given ticker owned by the user.
            int accountShares = GetAccountSharesForSymbol(seller, stockSymbol.ToUpper());
            // If user has less shares than the amount to sell, throw exception.
            if (accountShares < shareAmount)
            {
                throw new InsufficientSharesException(stockSymbol, accountShares, shareAmount);
            }
            // Get the current price of the stock using StockPriceService.
            double stockPrice = await _stockPriceService.GetStockPrice(stockSymbol.ToUpper());

            // Add a new sell transaction to the seller's account.
            seller.AssetTransactions.Add(new AssetTransaction()
            {
                Account = seller,
                Stock = new Stock()
                {
                    Symbol = stockSymbol.ToUpper(),
                    PricePerShare = stockPrice
                },
                DateProcessed = DateTime.Now,
                IsBuy = false,
                ShareAmount = shareAmount
            });

            // Update the seller's account balance.
            seller.Balance += shareAmount * stockPrice;

            // Save the updated seller's account.
            await _accountService.Update(seller.Id, seller);

            // Return the updated account.
            return seller;
        }

        /// <summary>
        /// Method to get the total amount of shares owned by the user for a given stock.
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="stockSymbol">The number of shares to sell.</param>
        /// <returns></returns>
        private int GetAccountSharesForSymbol(Account seller, string stockSymbol)
        {
            IEnumerable<AssetTransaction> accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Stock.Symbol == stockSymbol);
            return accountTransactionsForSymbol.Sum(a => a.IsBuy ? a.ShareAmount : -a.ShareAmount);
        }
    }
}
