namespace BattleCottage.Core.Exceptions
{
    public class RegisterException : BattleCottageException
    {
        public RegisterException() : base() { }
        public RegisterException(string message) : base(message) { }
        public RegisterException(string message, Exception innerException) : base(message, innerException) { }
    }
}