using StockTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;

        private readonly ObservableCollection<StockViewModel> _topStocks;

        public double AccountBalance => _assetStore.AccountBalance;
        public IEnumerable<StockViewModel> TopStocks => _topStocks;

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            _topStocks = new ObservableCollection<StockViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<StockViewModel> stockViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Stock.Symbol)
                .Select(g => new StockViewModel(g.Key, g.Sum(a => a.IsBuy ? a.ShareAmount : -a.ShareAmount)))
                .Where(a => a.ShareAmount > 0)
                .OrderByDescending(a => a.ShareAmount)
                .Take(3);
        
            _topStocks.Clear();
            foreach(StockViewModel stockViewModel in stockViewModels)
            {
                _topStocks.Add(stockViewModel);
            }
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }
    }
}
