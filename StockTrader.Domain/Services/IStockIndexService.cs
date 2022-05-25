using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public interface IStockIndexService
    {
        Task<StockIndex> GetStockIndex(StockIndexType indexType); 
    }
}
