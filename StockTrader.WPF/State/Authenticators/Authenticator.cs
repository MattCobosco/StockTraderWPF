using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.WPF.State.Accounts;
using System;
using System.Threading.Tasks;

namespace StockTrader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public Account CurrentAccount
        {
            get
            {
                return _accountStore.CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;
        public event Action StateChanged;

        public async Task Login(string username, string password)
        {
            CurrentAccount = await _authenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrentAccount = null;
        }

        public Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
