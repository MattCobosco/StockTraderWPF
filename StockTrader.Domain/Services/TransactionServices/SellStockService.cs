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

        public async Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount)
        {
            int accountShares = GetAccountSharesForSymbol(seller, stockSymbol.ToUpper());
            if (accountShares < shareAmount)
            {
                throw new InsufficientSharesException(stockSymbol, accountShares, shareAmount);
            }

            double stockPrice = await _stockPriceService.GetStockPrice(stockSymbol.ToUpper());

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

            seller.Balance += shareAmount * stockPrice;

            await _accountService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string stockSymbol)
        {
            IEnumerable<AssetTransaction> accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Stock.Symbol == stockSymbol);
            return accountTransactionsForSymbol.Sum(a => a.IsBuy ? a.ShareAmount : -a.ShareAmount);
        }
    }
}
