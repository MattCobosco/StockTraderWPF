namespace StockTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; set; }
        public int ActualShares { get; set; }
        public int RequiredShares { get; set; }

        public InsufficientSharesException(string symbol, int actualShares, int requiredShares)
        {
            Symbol = symbol;
            ActualShares = actualShares;
            RequiredShares = requiredShares;
        }
        public InsufficientSharesException(string symbol, int actualShares, int requiredShares, string? message) : base(message)
        {
            Symbol = symbol;
            ActualShares = actualShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string symbol, int acutalShares, int requiredShares, string? message, Exception? innerException) : base(message, innerException)
        {
            Symbol = symbol;
            ActualShares = acutalShares;
            RequiredShares = requiredShares;
        }
    }
}
