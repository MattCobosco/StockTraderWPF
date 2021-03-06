namespace StockTrader.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for when a user provides an invalid username.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public string Username { get; }
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
