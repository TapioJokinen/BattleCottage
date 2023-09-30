namespace BattleCottage.Core.Exceptions
{
    public class TokenException : BattleCottageException
    {
        public TokenException() : base() { }
        public TokenException(string message) : base(message) { }
        public TokenException(string message, Exception innerException) : base(message, innerException) { }
    }
}