namespace StockTrader.Domain.Services
{
    public interface IStockPriceService
    {
        Task<double> GetStockPrice(string symbol);
    }
}