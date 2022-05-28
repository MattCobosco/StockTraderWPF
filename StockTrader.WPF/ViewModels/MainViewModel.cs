using StockTrader.WPF.Commands;
using StockTrader.WPF.State.Authenticators;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace StockTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;

        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IAuthenticator authenticator)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;
            Authenticator = authenticator;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
