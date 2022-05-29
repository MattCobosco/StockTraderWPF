namespace StockTrader.WPF.ViewModels
{
    public class AssetViewModel : ViewModelBase
    {
        public string Symbol { get; }
        public int ShareAmount { get; }

        public AssetViewModel(string symbol, int shareAmount)
        {
            Symbol = symbol;
            ShareAmount = shareAmount;
        }
    }
}