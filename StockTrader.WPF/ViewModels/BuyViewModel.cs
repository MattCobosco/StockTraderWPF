using StockTrader.Domain.Services;
using StockTrader.Domain.Services.TransactionServices;
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

        private int _stockAmountToBuy;
        public int StockAmountToBuy
        {
            get
            {
                return _stockAmountToBuy;
            }
            set
            {
                _stockAmountToBuy = value;
                OnPropertyChanged(nameof(StockAmountToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice
        {
            get
            {
                return StockAmountToBuy * StockPrice;
            }
        }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService);
        }
    }
}
