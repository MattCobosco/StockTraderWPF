using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public interface IMajorIndexService
    {
        /// <summary>
        /// Get the major index.
        /// </summary>
        /// <param name="indexType">One of three index types.</param>
        /// <returns>Major index based on type.</returns>
        Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    }
}