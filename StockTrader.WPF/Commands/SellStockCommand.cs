using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services.TransactionServices;
using StockTrader.WPF.State.Accounts;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
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
            _sellViewModel.StatusMessage = string.Empty;
            _sellViewModel.ErrorMessage = string.Empty;

            try
            {
                string symbol = _sellViewModel.Symbol;
                int stockAmountToSell = _sellViewModel.ShareAmountToSell;
                string shareOrShares = stockAmountToSell == 1 ? "share" : "shares";

                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, stockAmountToSell);
                _accountStore.CurrentAccount = account;

                _sellViewModel.StatusMessage = $"{stockAmountToSell} {shareOrShares} of {symbol} sold successfully.";
            }
            catch(InsufficientSharesException ise)
            {
                _sellViewModel.ErrorMessage = $"Account has insufficient shares to sell {ise.RequiredShares}. You only own {ise.ActualShares}.";
            }
            catch(InvalidSymbolException)
            {
                _sellViewModel.ErrorMessage = $"Invalid symbol. Please check it and try again.";
            }
            catch (Exception)
            {
                _sellViewModel.ErrorMessage = "An error occured. Please try again";
            }
        }
    }
}
