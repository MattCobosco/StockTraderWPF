using StockTrader.Domain.Models;
using System;

namespace StockTrader.WPF.State.Accounts
{
    /// <summary>
    /// Implementation of IAccountStore.
    /// </summary>
    public class AccountStore : IAccountStore
    {
        // Account of the current user.
        private Account _currentAccount;

        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
