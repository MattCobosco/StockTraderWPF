using StockTrader.Domain.Models;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.WPF.State.Accounts;
using StockTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    public class BuyStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly BuyViewModel _buyViewModel;
        public readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }
        /// <summary>
        /// User BuyStockService to get a new state of the currently logged in account on stock purchase (added transaction, reduced balance) and updates it.
        /// </summary>
        public async void Execute(object? parameter)
        {
            try
            {
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, _buyViewModel.Symbol, _buyViewModel.StockAmountToBuy);
                _accountStore.CurrentAccount = account;

                MessageBox.Show($"{_buyViewModel.StockAmountToBuy} shares of {_buyViewModel.Symbol} bought successfully.", "Success!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}