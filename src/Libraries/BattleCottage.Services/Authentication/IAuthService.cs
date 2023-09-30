using BattleCottage.Services.Token;

namespace BattleCottage.Services.Authentication
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(AuthCredentials credentials);

        public Task Register(RegisterCredentials credentials);

        public Task<TokenModel> RefreshAccessToken(TokenModel tokens);
    }
}
