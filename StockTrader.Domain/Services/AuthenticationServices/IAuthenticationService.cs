using StockTrader.Domain.Models;

namespace StockTrader.Domain.Services
{
    public enum RegistrationResult
    {
        Success,
        PasswordsNoMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<Account> Login(string username, string password);
    }
}