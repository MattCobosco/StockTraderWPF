namespace StockTrader.WPF.ViewModels
{
    public class StockViewModel : ViewModelBase
    {
        public string Symbol { get; }
        public int ShareAmount { get; }

        public StockViewModel(string symbol, int shareAmount)
        {
            Symbol = symbol;
            ShareAmount = shareAmount;
        }
    }
}
