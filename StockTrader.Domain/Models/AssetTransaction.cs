namespace StockTrader.Domain.Models
{
    public class AssetTransaction : DomainObject
    {
        public Account Account { get; set; }
        public bool IsBuy { get; set; }
        public Stock Stock { get; set; }
        public int ShareAmount { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
