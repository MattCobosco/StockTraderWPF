using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyStock(Account buyer, string stockSymbol, int shareAmount);
    }
}