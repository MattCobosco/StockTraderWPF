using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.WPF.State.Accounts;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to sell stock, bound to a button in the UI.
    /// </summary>
    public class SellStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel sellViewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            // Clear messages.
            _sellViewModel.StatusMessage = string.Empty;
            _sellViewModel.ErrorMessage = string.Empty;

            try
            {
                // Get stock symbol from the view model.
                string symbol = _sellViewModel.Symbol;
                // Get number of shares to sell from the view model.
                int stockAmountToSell = _sellViewModel.ShareAmountToSell;
                // Get current word form based on stock amount.
                string shareOrShares = stockAmountToSell == 1 ? "share" : "shares";

                // Get the updated seller's account after the transaction using the SellStockService.
                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, stockAmountToSell);
                // Set the updated account as the current account.
                _accountStore.CurrentAccount = account;
                // Set the status message.
                _sellViewModel.StatusMessage = $"{stockAmountToSell} {shareOrShares} of {symbol} sold successfully.";
            }
            catch(InsufficientSharesException ise)
            {
                // Set the error message in case the user tries to sell more shares than they own.
                _sellViewModel.ErrorMessage = $"Account has insufficient shares to sell {ise.RequiredShares}. You only own {ise.ActualShares}.";
            }
            catch(InvalidSymbolException)
            {
                // Set the error message in case the user tries to sell a stock that doesn't exist.
                _sellViewModel.ErrorMessage = $"Invalid symbol. Please check it and try again.";
            }
            catch (Exception)
            {
                // Set the error message in case an unexpected error.
                _sellViewModel.ErrorMessage = "An error occured. Please try again";
            }
        }
    }
}
