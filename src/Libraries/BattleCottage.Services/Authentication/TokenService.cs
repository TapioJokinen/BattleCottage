using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattleCottage.Services.Authentication
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
        /// Generates a JWT.
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public JwtSecurityToken GetToken(IList<Claim> authClaims)
        {
            string? secret = _configuration.GetValue<string>("JWT:Secret") ?? throw new ArgumentException("JWT:Secret was not found.");

            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(secret));

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
        /// Returns JWT expiration date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetExpirationDate() => DateTime.Now.AddHours(_lifeTimeHours);

        /// <summary>
        /// Returns JWT ValidIssuer.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetIssuer()
        {
            return _configuration["JWT:ValidIssuer"] ?? throw new ArgumentException("JWT:ValidIssuer not found.");
        }

        /// <summary>
        /// Returns JWT ValidAudience.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetAudience()
        {
            return _configuration["JWT:ValidAudience"] ?? throw new ArgumentException("JWT:ValidAudience not found.");
        }

        /// <summary>
        /// Returns JWT cookie 'X-Token's domain name.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetCookieDomain()
        {
            return _configuration["JWT:CookieOptions:Domain"] ?? throw new ArgumentException("JWT:CookieOptions:Domain not found.");
        }

        /// <summary>
        /// Returns JWT cookie 'X-Token's HttpOnly value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool GetCookieHttpOnly()
        {
            if (_configuration["JWT:CookieOptions:HttpOnly"] == null)
            {
                throw new ArgumentException("JWT:CookieOptions:HttpOnly not found.");
            }

            return _configuration.GetValue<bool>("JWT:CookieOptions:HttpOnly");
        }

        /// <summary>
        /// Returns JWT cookie 'X-Token's SameSite value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
        /// Returns JWT cookie 'X-Token's Secure value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool GetCookieSecure()
        {
            if (_configuration["JWT:CookieOptions:Secure"] == null)
            {
                throw new ArgumentException("JWT:CookieOptions:Secure not found.");
            }

            return _configuration.GetValue<bool>("JWT:CookieOptions:Secure");
        }

        public string GetCookieName()
        {
            return _cookieName;
        }
    }
}
