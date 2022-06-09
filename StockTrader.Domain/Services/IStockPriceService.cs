namespace StockTrader.Domain.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the price of a stock.
        /// </summary>
        /// <param name="symbol">Stock symbol.</param>
        /// <returns>Stock price.</returns>
        Task<double> GetStockPrice(string symbol);
    }
}