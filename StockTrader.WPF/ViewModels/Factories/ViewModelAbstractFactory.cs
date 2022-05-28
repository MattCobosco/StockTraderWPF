using StockTrader.WPF.State.Navigators;
using System;

namespace StockTrader.WPF.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private IViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private IViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private BuyViewModel _buyViewModel;

        public ViewModelAbstractFactory(
            IViewModelFactory<HomeViewModel> homeViewModelFactory,
            IViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _homeViewModelFactory.CreateViewModel();
                case ViewType.Portfolio:
                    return _portfolioViewModelFactory.CreateViewModel();
                case ViewType.Buy:
                    return _buyViewModel;
                default:
                    throw new ArgumentException("ViewType does not have the requested ViewModel", "viewType");
            }
        }
    }
}