using StockTrader.Domain.Services;
using StockTrader.WPF.State.Authenticators;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to register a new account, bound to a button in the register view.
    /// </summary>
    public class RegisterCommand : ICommand
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = registerViewModel;
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
            // Get the registration result using the Authenticator.
            RegistrationResult result = await _authenticator.Register(_registerViewModel.Email, _registerViewModel.Username, _registerViewModel.Password, _registerViewModel.ConfirmPassword);

            try
            {
                // Actions based on RegistrationResult
                switch (result)
                {
                    // If Success, Renavigator renavigates to LoginView.
                    case RegistrationResult.Success:
                        _renavigator.Renavigate();
                        break;
                    // If PasswordsNoMatch, show a MessageBox.
                    case RegistrationResult.PasswordsNoMatch:
                        MessageBox.Show("Passwords do not match.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    // If EmailAlreadyExists, show a MessageBox.
                    case RegistrationResult.EmailAlreadyExists:
                        MessageBox.Show("Email already exists.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    // If UsernameAlreadyExists, show a MessageBox.
                    case RegistrationResult.UsernameAlreadyExists:
                        MessageBox.Show("Username already exists.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    default:
                    // Fallback value in case the result is different, show a MessageBox.
                        MessageBox.Show("Unknown error.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions and show their message in a MessageBox.
                MessageBox.Show(ex.Message, "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
