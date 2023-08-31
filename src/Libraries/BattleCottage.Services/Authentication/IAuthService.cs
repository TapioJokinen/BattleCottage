using BattleCottage.Services.Models;
using BattleCottage.Services.Models.ConstrollerResponses;

namespace BattleCottage.Services.Authentication
{
    public interface IAuthService
    {
        public Task<LoginResponse?> Login(LoginCredentials credentials);

        public Task<RegisterError?> Register(RegisterCredentials credentials);

        public Task<TokenModel?> RefreshAccessToken(TokenModel tokens);
    }
}
