using StockTrader.Domain.Exceptions;
using StockTrader.WPF.State.Authenticators;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to log in to an existing account, bound to a button in the login view.
    /// </summary>
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            // Clear any previous errors.
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
                // Attempt to log in.
                await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);

                // Renavigate to the appropriate view.
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                // Set the error message in case the username was invalid.
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                // Set the error message in case the password was invalid.
                _loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                // Set the error message in case of an unexpected error.
                _loginViewModel.ErrorMessage = "Login failed.";
            }
        }
    }
}
