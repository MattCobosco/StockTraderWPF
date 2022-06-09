using StockTrader.WPF.ViewModels;
using System;

namespace StockTrader.WPF.State.Navigators
{
    /// <summary>
    /// Implements INavigator.
    /// </summary>
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
