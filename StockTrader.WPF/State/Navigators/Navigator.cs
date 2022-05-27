using StockTrader.WPF.Commands;
using StockTrader.WPF.Models;
using StockTrader.WPF.ViewModels;
using StockTrader.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace StockTrader.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(IViewModelAbstractFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);

        }
    }
}
