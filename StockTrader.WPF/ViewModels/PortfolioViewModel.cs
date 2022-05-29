using StockTrader.WPF.State.Assets;
using System.Collections.Generic;
using System.Linq;

namespace StockTrader.WPF.ViewModels
{
	public class PortfolioViewModel : ViewModelBase
	{
        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }
    }
}