using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to update current view model, used by the Navigator.
    /// </summary>
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
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
            // Check if parameter is ViewType. 
            if (parameter is ViewType)
            {
                // Convert the parameter to ViewType.
                ViewType viewType = (ViewType)parameter;

                // Create a view model using ViewModelFactory and set it as the CurrentViewModel in the Navigator.
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}