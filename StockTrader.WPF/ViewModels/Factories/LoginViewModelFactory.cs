namespace StockTrader.WPF.ViewModels.Factories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel();

        }
    }
}
