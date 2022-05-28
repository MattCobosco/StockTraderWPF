using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string username, string password, string confirmPassword);
        Task<Account> Login(string username, string password);
    }
}