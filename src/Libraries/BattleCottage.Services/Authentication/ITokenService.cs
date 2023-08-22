using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BattleCottage.Services.Authentication
{
    public interface ITokenService
    {
        JwtSecurityToken GetToken(IList<Claim> authClaims);

        DateTime GetExpirationDate();

        string GetIssuer();

        string GetAudience();

        string GetCookieDomain();

        bool GetCookieHttpOnly();

        SameSiteMode GetCookieSameSite();

        bool GetCookieSecure();

        string GetCookieName();
    }
}
