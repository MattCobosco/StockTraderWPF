using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.WPF.ViewModels
{
    public class MajorIndexViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;

        private MajorIndex _dowJones;
        public MajorIndex DowJones 
        {
            get 
            {
                return _dowJones;
            }
            set
            {
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }

        private MajorIndex _nASDAQ;
        public MajorIndex NASDAQ 
        {
            get
            {
                return _nASDAQ;
            }    
            set
            {
                _nASDAQ = value;
                OnPropertyChanged(nameof(NASDAQ));
            }
        }

        private MajorIndex _sP500;
        public MajorIndex SP500 
        { 
            get
            {
                return _sP500;
            }
            set
            {
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }

        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    DowJones = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.NASDAQ).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    NASDAQ = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    SP500 = task.Result;
                }
            });
        }

    }
}
