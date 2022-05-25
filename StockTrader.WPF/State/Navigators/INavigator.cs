using StockTrader.WPF.ViewModels;
using System.Windows.Input;

namespace StockTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Portfolio
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
