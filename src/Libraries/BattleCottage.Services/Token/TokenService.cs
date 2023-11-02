using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BattleCottage.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private const string _cookieName = "X-Token";

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GetToken(IList<Claim> authClaims)
        {
            SymmetricSecurityKey authSigningKey = GetSymmetricSecurityKey();

            var token = new JwtSecurityToken(
                issuer: GetIssuer(),
                audience: GetAudience(),
                expires: GetAccessTokenExpiryTime(),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public string GetIssuer()
        {
            return _configuration["JWT:ValidIssuer"] ?? throw new ArgumentException("JWT:ValidIssuer not found.");
        }

        public string GetAudience()
        {
            return _configuration["JWT:ValidAudience"] ?? throw new ArgumentException("JWT:ValidAudience not found.");
        }

        private string GetSecret()
        {
            return _configuration.GetValue<string>("JWT:Secret")
                ?? throw new ArgumentException("JWT:Secret was not found.");
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.UTF8.GetBytes(GetSecret()));
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecret())),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
            );

            if (
                securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public DateTime GetAccessTokenExpiryTime()
        {
            _ = int.TryParse(_configuration["JWT:AccessTokenLifeTimeInMinutes"], out int accessTokenLifeTimeInMinutes);

            return DateTime.UtcNow.AddMinutes(accessTokenLifeTimeInMinutes);
        }

        public DateTime GetRefreshTokenExpiryTime()
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenLifeTimeInDays"], out int RefreshTokenLifeTimeInDays);

            return DateTime.UtcNow.AddDays(RefreshTokenLifeTimeInDays);
        }
    }
}
