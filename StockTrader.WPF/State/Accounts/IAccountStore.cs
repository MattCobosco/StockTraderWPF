using StockTrader.Domain.Models;
using System;

namespace StockTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
