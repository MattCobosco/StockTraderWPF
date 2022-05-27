using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.WPF.ViewModels.Factories
{
    public interface IViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
