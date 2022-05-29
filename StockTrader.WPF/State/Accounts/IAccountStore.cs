using StockTrader.Domain.Models;

namespace StockTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
    }
}
