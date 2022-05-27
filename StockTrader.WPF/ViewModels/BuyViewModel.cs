using StockTrader.Domain.Services;
using StockTrader.WPF.Commands;
using System.Windows.Input;

namespace StockTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        private string _symbol;
        public string Symbol
        {
            get 
            { 
                return _symbol; 
            }
            set 
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }


        private double _stockPrice;
        public double StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
            }
        }

        public ICommand SearchSymbolCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
        }
    }
}
