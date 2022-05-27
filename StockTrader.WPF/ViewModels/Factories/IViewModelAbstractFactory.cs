using StockTrader.WPF.State.Navigators;

namespace StockTrader.WPF.ViewModels.Factories
{
    public interface IViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
