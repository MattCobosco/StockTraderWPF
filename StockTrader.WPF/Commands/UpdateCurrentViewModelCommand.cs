using StockTrader.API.Services;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels;
using StockTrader.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IViewModelAbstractFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}