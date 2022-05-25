using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MajorIndexViewModel MajorIndexViewMode { get; set; }

        public HomeViewModel(MajorIndexViewModel majorIndexViewModel)
        {
            MajorIndexViewMode = majorIndexViewModel;
        }
    }
}
