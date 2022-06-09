using StockTrader.Domain.Models;
using StockTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;

namespace StockTrader.WPF.State.Assets
{
    /// <summary>
    /// Stores the assets of the current user.
    /// </summary>
    public class AssetStore
    {
        // Current user's account.
        private readonly IAccountStore _accountStore;
        // Current account balance from database/0.
        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        // Current list of asset transactions from database/new list.
        public IEnumerable<AssetTransaction> AssetTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
