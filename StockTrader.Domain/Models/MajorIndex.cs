namespace StockTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        NASDAQ,
        SP500
    }
    public class MajorIndex
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
