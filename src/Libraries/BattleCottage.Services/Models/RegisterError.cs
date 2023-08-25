namespace BattleCottage.Services.Models
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
