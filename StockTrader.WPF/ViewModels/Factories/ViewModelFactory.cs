using StockTrader.WPF.State.Navigators;
using System;

namespace StockTrader.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
        private readonly CreateViewModel<SellViewModel> _createSellViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, 
            CreateViewModel<LoginViewModel> createLoginViewModel, 
            CreateViewModel<BuyViewModel> createBuyViewModel,
            CreateViewModel<SellViewModel> createSellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createBuyViewModel = createBuyViewModel;
            _createSellViewModel = createSellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Portfolio:
                    return _createPortfolioViewModel();
                case ViewType.Buy:
                    return _createBuyViewModel();
                case ViewType.Sell:
                    return _createSellViewModel();
                default:
                    throw new ArgumentException("ViewType does not have the requested ViewModel", "viewType");
            }
        }
    }
}