namespace StockTrader.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for when a user provides an invalid stock symbol.
    /// </summary>
    public class InvalidSymbolException : Exception
    {
        public string Symbol { get; }

        public InvalidSymbolException(string symbol)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string symbol, string? message) : base(message)
        {
            Symbol = symbol;
        }

        public InvalidSymbolException(string symbol, string? message, Exception? innerException) : base(message, innerException)
        {
            Symbol = symbol;
        }
    }
}
