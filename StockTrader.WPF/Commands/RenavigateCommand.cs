using StockTrader.WPF.State.Navigators;
using System;
using System.Windows.Input;

namespace StockTrader.WPF.Commands
{
    /// <summary>
    /// A command to renavigate between Login and Register and Home views, bound to 3 buttons in the UI (login, register x2).
    /// </summary>
    public class RenavigateCommand : ICommand
    {
        private readonly IRenavigator _renavigator;

        public RenavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // Use Renavigator to navigate to the appropriate view.
            _renavigator.Renavigate();
        }
    }
}
