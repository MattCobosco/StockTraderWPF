using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType); 
    }
}
