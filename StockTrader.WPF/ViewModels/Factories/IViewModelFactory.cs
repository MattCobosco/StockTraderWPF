using StockTrader.WPF.State.Navigators;

namespace StockTrader.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}