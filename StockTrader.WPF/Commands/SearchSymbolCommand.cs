using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services;
using StockTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to search for a stock, bound to a button in the UI.
    /// </summary>
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
                // Get the current stock price using the StockPriceService.
                double stockPrice = await _stockPriceService.GetStockPrice(_viewModel.Symbol);
                // Convert the symbol to upper case and return it to the view model as SearchResultSymbol.
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                // Return the stock price to the view model as StockPrice.
                _viewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException ise)
            {
                // Set the error message in case the symbol does not exist.
                _viewModel.ErrorMessage = $"Symbol {ise.Symbol} does not exist.";
            }
            catch (Exception)
            {
                // Set the error message in case of any other exception.
                _viewModel.ErrorMessage = "Failed to get symbol information.";
            }
        }
    }
}
