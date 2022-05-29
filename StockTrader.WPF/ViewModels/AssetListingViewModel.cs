using StockTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;
        private readonly System.Collections.ObjectModel.ObservableCollection<AssetViewModel> _assets;

        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets) { }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _filterAssets = filterAssets;
            _assets = new System.Collections.ObjectModel.ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Stock.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsBuy ? a.ShareAmount : -a.ShareAmount)))
                .Where(a => a.ShareAmount > 0)
                .OrderByDescending(a => a.ShareAmount);

            assetViewModels = _filterAssets(assetViewModels);

            _assets.Clear();
            foreach (AssetViewModel viewModel in assetViewModels)
            {
                _assets.Add(viewModel);
            }
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }
    }
}
