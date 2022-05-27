namespace StockTrader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : IViewModelFactory<HomeViewModel>
    {
        private IViewModelFactory<MajorIndexListingViewModel> _majorIndexViewModelFactory;

        public HomeViewModelFactory(IViewModelFactory<MajorIndexListingViewModel> majorIndexViewModelFactory)
        {
            _majorIndexViewModelFactory = majorIndexViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_majorIndexViewModelFactory.CreateViewModel());
        }
    }
}
