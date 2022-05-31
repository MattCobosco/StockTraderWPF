using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.WPF.State.Accounts;
using StockTrader.WPF.ViewModels;
using System;
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
        
        public async void Execute(object? parameter)
        {
            _buyViewModel.StatusMessage = string.Empty;
            _buyViewModel.ErrorMessage = string.Empty;
                
            try
            {
                string symbol = _buyViewModel.Symbol;
                int stockAmountToBuy = _buyViewModel.StockAmountToBuy;
                string shareOrShares = stockAmountToBuy == 1 ? "share" : "shares";

                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, symbol, stockAmountToBuy);
                _accountStore.CurrentAccount = account;

               _buyViewModel.StatusMessage = $"{stockAmountToBuy} {shareOrShares} of {symbol} bought successfully.";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Insuffcient funds. Please deposit more money and try again.";
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Invalid symbol. Please check it and try again.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "An error occurred. Please try again.";
            }
        }
    }
}