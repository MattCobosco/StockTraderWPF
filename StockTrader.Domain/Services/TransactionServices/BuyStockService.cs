using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string stockSymbol, int shareAmount)
        {
            string stockSymbolToUpper = stockSymbol.ToUpper();
            double stockPrice = await _stockPriceService.GetStockPrice(stockSymbolToUpper);
            double transactionPrice = shareAmount * stockPrice;

            if (stockPrice * shareAmount > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

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

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await _accountService.Update(buyer.Id, buyer);

            return buyer;
        }
    }
}