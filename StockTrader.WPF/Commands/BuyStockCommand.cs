using StockTrader.Domain.Models;
using StockTrader.Domain.Services.TransactionServices;
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

        private BuyViewModel _buyViewModel;
        public IBuyStockService _buyStockService;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                Account account = await _buyStockService.BuyStock(new Account()
                {
                    Id = 1,
                    Balance = 500,
                    AssetTransactions = new List<AssetTransaction>(),
                },
                _buyViewModel.Symbol, _buyViewModel.StockAmountToBuy);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}