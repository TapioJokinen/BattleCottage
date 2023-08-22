using System.IdentityModel.Tokens.Jwt;
using BattleCottage.Services.Models;

namespace BattleCottage.Services.Authentication
{
    public interface IAuthService
    {
        public Task<JwtSecurityToken?> Login(LoginCredentials credentials);

        public Task<string?> Register(RegisterCredentials credentials);
    }
}
