namespace StockTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public int ActualShares { get; set; }
        public int RequiredShares { get; set; }

        public InsufficientSharesException(int actualShares, int requiredShares)
        {
            ActualShares = actualShares;
            RequiredShares = requiredShares;
        }
        public InsufficientSharesException(int actualShares, int requiredShares, string? message) : base(message)
        {
            ActualShares = actualShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(int acutalShares, int requiredShares, string? message, Exception? innerException) : base(message, innerException)
        {
            ActualShares = acutalShares;
            RequiredShares = requiredShares;
        }
    }
}
