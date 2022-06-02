using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private ISearchSymbolViewModel _viewModel;
        private IStockPriceService _stockPriceService;

        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(ISearchSymbolViewModel viewModel, IStockPriceService stockPriceService)
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
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                _viewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException ise)
            {
                _viewModel.ErrorMessage = $"Symbol {ise.Symbol} does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to get symbol information.";
            }
        }
    }
}
