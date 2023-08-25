using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BattleCottage.Services.Token
{
    public interface ITokenService
    {
        string GetAudience();

        string GetCookieDomain();

        bool GetCookieHttpOnly();

        SameSiteMode GetCookieSameSite();

        bool GetCookieSecure();

        string GetCookieName();

        DateTime GetExpirationDate();

        string GetIssuer();

        JwtSecurityToken GetToken(IList<Claim> authClaims);

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }

}
