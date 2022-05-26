using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.Domain.Services
{
    public interface IStockPriceService
    {
        Task<double> GetStockPrice(string symbol);
    }
}
