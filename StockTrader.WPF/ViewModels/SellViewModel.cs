using StockTrader.Domain.Services;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.WPF.Commands;
using StockTrader.WPF.State.Accounts;
using StockTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        private AssetViewModel _selectedAsset;
        public AssetViewModel SelectedAsset
        {
            get 
            { 
                return _selectedAsset; 
            }
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
            }
        }

        public string Symbol => SelectedAsset?.Symbol;

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get 
            { 
                return _searchResultSymbol; 
            }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
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
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _shareAmountToSell;
        public int ShareAmountToSell
        {
            get { return _shareAmountToSell; }
            set
            {
                _shareAmountToSell = value;
                OnPropertyChanged(nameof(ShareAmountToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice => ShareAmountToSell * StockPrice;

        public MessageViewModel ErrorMessageViewModel { get; set; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public MessageViewModel StatusMessageViewModel { get; }
        
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public SellViewModel(AssetStore assetStore, 
            IStockPriceService stockPriceService, 
            IAccountStore accountStore, 
            ISellStockService sellStockService)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);

            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }
    }
}
