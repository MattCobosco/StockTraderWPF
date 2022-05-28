using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using System.Threading.Tasks;

namespace StockTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<bool> Login(string username, string password);
        void Logout();
    }
}
