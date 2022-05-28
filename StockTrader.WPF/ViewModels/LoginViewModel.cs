using StockTrader.WPF.Commands;
using StockTrader.WPF.State.Authenticators;
using StockTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace StockTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            LoginCommand = new LoginCommand(this, authenticator, renavigator);
        }
    }
}
