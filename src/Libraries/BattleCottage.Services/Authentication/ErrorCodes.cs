namespace BattleCottage.Services.Authentication
{
    public static class ErrorCodes
    {
        public const string EmptyPassword = "Password must not be empty.";
        public const string InvalidEmailFormat = "The provided email was not in correct format.";
        public const string PasswordMissingAlphanumeric = "Passwords must have at least one non alphanumeric character.";
        public const string PasswordMissingLowercase = "Passwords must have at least one lowercase ('a'-'z').";
        public const string PasswordMissingNumeric = "Passwords must have at least one digit ('0'-'9').";
        public const string PasswordMissingUppercase = "Passwords must have at least one uppercase ('A'-'Z').";
        public const string PasswordsDoNotMatch = "Passwords did not match.";
        public const string PasswordTooShort = "Passwords must be at least 6 characters.";
        public const string UserAlreadyExists = "A User with this email already exists.";
    }
}
