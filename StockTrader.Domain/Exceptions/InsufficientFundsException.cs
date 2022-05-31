namespace StockTrader.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public double AccountBalance { get; }
        public double RequiredBalance { get; }

        public InsufficientFundsException(double accountBalance, double requiredBalance)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(double accountBalance, double requiredBalance, string? message) : base(message)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(double accountBalance, double requiredBalance, string? message, Exception? innerException) : base(message, innerException)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }
    }
}