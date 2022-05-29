using StockTrader.Domain.Models;

namespace StockTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public Account CurrentAccount { get; set; }
    }
}
