using BattleCottage.Services.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BattleCottage.Services.Authentication
{
    public interface IAuthService
    {
        public Task<JwtSecurityToken?> Login(LoginCredentials credentials);

        public Task<RegisterError?> Register(RegisterCredentials credentials);

        public bool VerifyToken(string token);
    }
}
