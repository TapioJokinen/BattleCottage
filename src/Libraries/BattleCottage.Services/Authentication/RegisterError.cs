namespace BattleCottage.Services.Authentication
{
    public class RegisterError
    {
        public RegisterError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
