using StockTrader.Domain.Models;
using System;

namespace StockTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        /// <summary>
        /// Account of the current user.
        /// </summary>
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}
