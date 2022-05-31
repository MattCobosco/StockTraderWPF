using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        Task<Account> SellStock(Account seller, string stockSymbol, int shareAmount);
    }
}
