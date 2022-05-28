using StockTrader.WPF.State.Navigators;

namespace StockTrader.WPF.ViewModels.Factories
{
    public interface IRootViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}