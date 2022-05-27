using Microsoft.Extensions.DependencyInjection;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.EntityFramework;
using StockTrader.EntityFramework.Services;
using StockTrader.WPF.State.Navigators;
using StockTrader.WPF.ViewModels;
using StockTrader.YahooFinanceAPI.Services;
using System;
using System.Windows;

namespace StockTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = new MainWindow();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<StockTraderDbContextFactory>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}