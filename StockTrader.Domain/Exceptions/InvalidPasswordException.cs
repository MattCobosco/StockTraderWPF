namespace StockTrader.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for when a user provides an invalid password.
    /// </summary>
    public class InvalidPasswordException : Exception
    {
        public string Username { get; }
        public string Password { get; }

        public InvalidPasswordException(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string username, string password, string? message) : base(message)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string username, string password, string? message, Exception? innerException) : base(message, innerException)
        {
            Username = username;
            Password = password;
        }
    }
}
