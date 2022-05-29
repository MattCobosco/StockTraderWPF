using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.WPF.ViewModels
{
    public class StockViewModel
    {
        public string Symbol { get; }
        public int ShareAmount { get; }

        public StockViewModel(string symbol, int shareAmount)
        {
            Symbol = symbol;
            ShareAmount = shareAmount;
        }
    }
}
