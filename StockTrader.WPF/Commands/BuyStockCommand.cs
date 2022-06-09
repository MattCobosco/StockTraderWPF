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
    /// A command to buy stock, bound to a button in the UI.
    /// </summary>
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

        public async void Execute(object? parameter)
        {
            // Clear messages.
            _buyViewModel.StatusMessage = string.Empty;
            _buyViewModel.ErrorMessage = string.Empty;

            try
            {
                // Get stock symbol from the view model.
                string symbol = _buyViewModel.Symbol;
                // Get number of shares to buy from the view model.
                int stockAmountToBuy = _buyViewModel.StockAmountToBuy;
                // Get the correct word form based on stock amount.
                string shareOrShares = stockAmountToBuy == 1 ? "share" : "shares";

                // Get the updated buyer's account after the transaction.
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, symbol, stockAmountToBuy);
                // Set the updated account as the current account.
                _accountStore.CurrentAccount = account;
                // Set the status message.
                _buyViewModel.StatusMessage = $"{stockAmountToBuy} {shareOrShares} of {symbol} bought successfully.";
            }
            catch (InsufficientFundsException)
            {
                // Set the error message in case the user does not have enough funds.
                _buyViewModel.ErrorMessage = "Insuffcient funds. Please deposit more money and try again.";
            }
            catch (InvalidSymbolException)
            {
                // Set the error message in case the user enters an invalid stock symbol.
                _buyViewModel.ErrorMessage = "Invalid symbol. Please check it and try again.";
            }
            catch (Exception)
            {
                // Set the error message in case of an unexpected error.
                _buyViewModel.ErrorMessage = "An error occurred. Please try again.";
            }
        }
    }
}