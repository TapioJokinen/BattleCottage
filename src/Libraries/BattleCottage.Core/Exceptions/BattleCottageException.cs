namespace BattleCottage.Core.Exceptions
{
    public class BattleCottageException : Exception
    {
        public BattleCottageException() : base()
        {

        }

        public BattleCottageException(string message) : base(message)
        {

        }

        public BattleCottageException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}