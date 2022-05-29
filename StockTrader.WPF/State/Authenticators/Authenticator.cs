﻿using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.WPF.Models;
using StockTrader.WPF.State.Accounts;
using System;
using System.Threading.Tasks;

namespace StockTrader.WPF.State.Authenticators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
                OnPropertyChanged(nameof(CurrentAccount));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public Authenticator(IAccountStore accountStore)
        {
            _accountStore = accountStore;
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                CurrentAccount = await _authenticationService.Login(username, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
