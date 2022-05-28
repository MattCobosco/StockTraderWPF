namespace StockTrader.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; set; }
        public UserNotFoundException(string username)
        {
            Username = username;
        }

        public UserNotFoundException(string username, string? message) : base(message)
        {
            Username = username;
        }

        public UserNotFoundException(string username, string? message, Exception? innerException) : base(message, innerException)
        {
            Username = username;
        }
    }
}
