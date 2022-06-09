using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    /// <summary>
    /// Implements IBuyStockService.
    /// </summary>
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        /// <summary>
        /// Buy a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="stockSymbol">The symbol of a stock bought.</param>
        /// <param name="shareAmount">The number of shares to buy.</param>
        /// <returns>The updated account after the buy transaction.</returns>
        /// <exception cref="InsufficientFundsException">Thrown if the account has insufficient balance.</exception>
        /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
        public async Task<Account> BuyStock(Account buyer, string stockSymbol, int shareAmount)
        {
            // If the stock symbol is invalid, throw an exception.
            if (string.IsNullOrEmpty(stockSymbol))
            {
                throw new InvalidSymbolException(stockSymbol);
            }

            // Converts stock symbol to uppercase.
            string stockSymbolToUpper = stockSymbol.ToUpper();

            // Get the current price of the stock using StockPriceService.
            double stockPrice = await _stockPriceService.GetStockPrice(stockSymbolToUpper);
            double transactionPrice = shareAmount * stockPrice;

            // If the account has insufficient balance, throw an exception.
            if (stockPrice * shareAmount > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            // Add create a new buy transaction.
            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Stock = new Stock()
                {
                    PricePerShare = stockPrice,
                    Symbol = stockSymbolToUpper,
                },
                DateProcessed = DateTime.Now,
                ShareAmount = shareAmount,
                IsBuy = true
            };

            // Add the transaction to the account.
            buyer.AssetTransactions.Add(transaction);
            // Update the buyer's account balance.
            buyer.Balance -= transactionPrice;

            // Save the updated buyer's account.
            await _accountService.Update(buyer.Id, buyer);

            // Return the updated account.
            return buyer;
        }
    }
}