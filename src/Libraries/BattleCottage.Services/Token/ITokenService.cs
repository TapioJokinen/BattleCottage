using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BattleCottage.Services.Token
{
    public interface ITokenService
    {
        DateTime GetAccessTokenExpiryTime();

        DateTime GetRefreshTokenExpiryTime();

        string GenerateRefreshToken();

        string GetAudience();

        string GetIssuer();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

        JwtSecurityToken GetToken(IList<Claim> authClaims);

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}
