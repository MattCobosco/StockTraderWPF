namespace StockTrader.Domain.Models
{
    public class AssetTransaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public bool IsBuy { get; set; }
        public Stock Stock { get; set; }
        public int ShareAmount { get; set; }
    }
}
