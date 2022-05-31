using StockTrader.Domain.Models;
using StockTrader.Domain.Exceptions;

namespace StockTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        public Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount)
        {
            throw new NotImplementedException();
        }
    }
}
