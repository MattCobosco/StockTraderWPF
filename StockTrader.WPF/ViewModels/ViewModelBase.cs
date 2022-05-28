using StockTrader.WPF.Models;

namespace StockTrader.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : ObservableObject
    {

    }
}
