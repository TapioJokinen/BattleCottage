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

        /// <summary>
        /// Generates a JSON Web Token (JWT) with the provided authentication claims and necessary token properties.
        /// The token's issuer, audience, expiration date, and signing credentials are set based on configuration and parameters.
        /// </summary>
        /// <param name="authClaims">A list of claims to be included in the token.</param>
        /// <returns>The generated JWT with the specified claims and properties.</returns>

        public JwtSecurityToken GetToken(IList<Claim> authClaims)
        {
            SymmetricSecurityKey authSigningKey = GetSymmetricSecurityKey();

            JwtSecurityToken token = new(
                issuer: GetIssuer(),
                audience: GetAudience(),
                expires: GetAccessTokenExpiryTime(),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        /// <summary>
        /// Retrieves the valid issuer (issuer) for the JSON Web Token (JWT).
        /// The issuer value is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>The valid issuer for the JWT.</returns>
        /// <exception cref="ArgumentException">Thrown when the ValidIssuer configuration is not found.</exception>

        public string GetIssuer()
        {
            return _configuration["JWT:ValidIssuer"] ?? throw new ArgumentException("JWT:ValidIssuer not found.");
        }

        /// <summary>
        /// Retrieves the valid audience (recipient) for the JSON Web Token (JWT).
        /// The audience value is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>The valid audience for the JWT.</returns>
        /// <exception cref="ArgumentException">Thrown when the ValidAudience configuration is not found.</exception>

        public string GetAudience()
        {
            return _configuration["JWT:ValidAudience"] ?? throw new ArgumentException("JWT:ValidAudience not found.");
        }


        /// <summary>
        /// Retrieves a secret string used for cryptographic operations, such as generating tokens.
        /// The secret is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>The secret string for cryptographic purposes.</returns>
        /// <exception cref="ArgumentException">Thrown when the secret configuration is not found.</exception>
        private string GetSecret()
        {
            return _configuration.GetValue<string>("JWT:Secret") ?? throw new ArgumentException("JWT:Secret was not found.");
        }

        /// <summary>
        /// Retrieves a symmetric security key generated from a secret string.
        /// This key can be used for cryptographic operations like authentication and data protection.
        /// </summary>
        /// <returns>A SymmetricSecurityKey instance derived from the secret.</returns>
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
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecret())),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new();

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
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
            _ = int.TryParse(_configuration["JWT:RefreshTokenLifeTimeInDays"], out int refreshTokenLifeTImeInDays);

            return DateTime.UtcNow.AddDays(refreshTokenLifeTImeInDays);
        }
    }
}
