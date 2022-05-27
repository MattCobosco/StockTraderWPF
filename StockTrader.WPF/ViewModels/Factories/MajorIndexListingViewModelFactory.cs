using StockTrader.Domain.Services;

namespace StockTrader.WPF.ViewModels.Factories
{
    public class MajorIndexListingViewModelFactory : IViewModelFactory<MajorIndexListingViewModel>
    {
        private IMajorIndexService _majorIndexService;

        public MajorIndexListingViewModelFactory(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public MajorIndexListingViewModel CreateViewModel()
        {
            return MajorIndexListingViewModel.LoadMajorIndexViewModel(_majorIndexService);
        }
    }
}
