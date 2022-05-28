using StockTrader.WPF.ViewModels;
using StockTrader.WPF.ViewModels.Factories;

namespace StockTrader.WPF.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, ViewModels.Factories.IViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
           _viewModelFactory = viewModelFactory;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
