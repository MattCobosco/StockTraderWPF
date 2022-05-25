namespace StockTrader.Domain.Models
{
    public enum StockIndexType
    {
        DowJones,
        NASDAQ,
        SP500
    }
    public class StockIndex
    {
        public string Price { get; set; }
        public double Changes { get; set; }
        public StockIndexType Type { get; set; }
    }
}
