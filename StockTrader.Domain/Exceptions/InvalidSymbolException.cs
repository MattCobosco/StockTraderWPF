using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        public string Symbol { get; set; }

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

        protected InvalidSymbolException(string symbol, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Symbol = symbol;
        }
    }
}
