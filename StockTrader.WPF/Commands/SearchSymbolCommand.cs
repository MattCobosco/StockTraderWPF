﻿using StockTrader.Domain.Services;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private BuyViewModel _viewModel;
        private IStockPriceService _stockPriceService;

        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(BuyViewModel viewModel, IStockPriceService stockPriceService)
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetStockPrice(_viewModel.Symbol);
                _viewModel.StockPrice = stockPrice;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
    }
}
