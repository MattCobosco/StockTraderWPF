using StockTrader.WPF.ViewModels;
using System;

namespace StockTrader.WPF.State.Navigators
{
    /// <summary>
    /// ViewTypes available.
    /// </summary>
    public enum ViewType
    {
        Login,
        Home,
        Portfolio,
        Buy,
        Sell
    }

    public interface INavigator
    {
        /// <summary>
        /// CurrentViewModel displayed in the app.
        /// </summary>
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
