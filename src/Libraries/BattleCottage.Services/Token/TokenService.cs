using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattleCottage.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private const int _lifeTimeHours = 24;

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
                expires: GetExpirationDate(),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        /// <summary>
        /// Calculates and retrieves the expiration date for the JSON Web Token (JWT).
        /// The expiration date is determined by adding the specified lifetime hours to the current date and time.
        /// </summary>
        /// <returns>The calculated expiration date for the JWT.</returns>

        public DateTime GetExpirationDate() => DateTime.Now.AddHours(_lifeTimeHours);

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
        /// Retrieves the domain for which the cookie is valid.
        /// The domain setting is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>The domain for which the cookie is valid.</returns>
        /// <exception cref="ArgumentException">Thrown when the Domain configuration is not found.</exception>

        public string GetCookieDomain()
        {
            return _configuration["JWT:CookieOptions:Domain"] ?? throw new ArgumentException("JWT:CookieOptions:Domain not found.");
        }

        /// <summary>
        /// Retrieves a boolean value indicating whether the cookie can be accessed through client side script.
        /// The HttpOnly setting is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>True if the cookie should be HttpOnly; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when the HttpOnly configuration is not found.</exception>

        public bool GetCookieHttpOnly()
        {
            if (_configuration["JWT:CookieOptions:HttpOnly"] == null)
            {
                throw new ArgumentException("JWT:CookieOptions:HttpOnly not found.");
            }

            return _configuration.GetValue<bool>("JWT:CookieOptions:HttpOnly");
        }

        /// <summary>
        /// Retrieves the SameSite attribute setting for the cookie, which controls when the cookie is sent in cross-origin requests.
        /// The SameSite setting is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>The SameSiteMode enum value representing the desired cookie behavior.</returns>
        /// <exception cref="ArgumentException">Thrown when the SameSite configuration is not found.</exception>
        public SameSiteMode GetCookieSameSite()
        {
            string sameSite = _configuration["JWT:CookieOptions:SameSite"] ?? throw new ArgumentException("JWT:CookieOptions:SameSite not found.");

            return sameSite switch
            {
                "Strict" => SameSiteMode.Strict,
                "Lax" => SameSiteMode.Lax,
                "None" => SameSiteMode.None,
                _ => SameSiteMode.Strict,
            };
        }

        /// <summary>
        /// Retrieves a boolean value indicating whether the cookie should only be transmitted over secure (HTTPS) connections.
        /// The secure setting is obtained from the application's configuration settings.
        /// </summary>
        /// <returns>True if the cookie should only be transmitted over secure connections; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when the secure configuration is not found.</exception>
        public bool GetCookieSecure()
        {
            if (_configuration["JWT:CookieOptions:Secure"] == null)
            {
                throw new ArgumentException("JWT:CookieOptions:Secure not found.");
            }

            return _configuration.GetValue<bool>("JWT:CookieOptions:Secure");
        }

        /// <summary>
        /// Retrieves the name of the cookie used for storing or identifying data.
        /// The cookie name is determined by the value assigned during object initialization.
        /// </summary>
        /// <returns>The name of the cookie.</returns>
        public string GetCookieName()
        {
            return _cookieName;
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
    }
}
