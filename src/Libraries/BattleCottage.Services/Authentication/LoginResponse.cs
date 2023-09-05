namespace BattleCottage.Services.Authentication
{
    public class LoginResponse
    {
        public required string Email { get; set; }

        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
