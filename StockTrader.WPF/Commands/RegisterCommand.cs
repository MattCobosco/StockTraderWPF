using StockTrader.Domain.Services;
using StockTrader.WPF.State.Authenticators;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
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
            RegistrationResult result = await _authenticator.Register(_registerViewModel.Email, _registerViewModel.Username, _registerViewModel.Password, _registerViewModel.ConfirmPassword);

            try
            {
                switch (result)
                {
                    case RegistrationResult.Success:
                        _renavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordsNoMatch:
                        MessageBox.Show("Passwords do not match.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        MessageBox.Show("Email already exists.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        MessageBox.Show("Username already exists.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    default:
                        MessageBox.Show("Unknown error.", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
