using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Models;
using BattleCottage.Services.Token;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BattleCottage.Web.Controllers.AuthController
{
    public class AuthController : APIControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository, ITokenService tokenService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _tokenService = tokenService;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            JwtSecurityToken? token = await _authService.Login(loginCredentials);

            if (token == null) return Unauthorized(new { Message = "Invalid credentials." });

            Response.Cookies.Append(
                _tokenService.GetCookieName(),
                new JwtSecurityTokenHandler().WriteToken(token),
                new CookieOptions()
                {
                    HttpOnly = _tokenService.GetCookieHttpOnly(),
                    SameSite = _tokenService.GetCookieSameSite(),
                    Domain = _tokenService.GetCookieDomain(),
                    Secure = _tokenService.GetCookieSecure(),
                    Expires = _tokenService.GetExpirationDate()
                }
                );

            return Ok(new { loginCredentials.Email });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/api/[controller]/register")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentials registerCredentials)
        {
            RegisterError? registerError = await _authService.Register(registerCredentials);

            if (registerError != null)
            {
                return BadRequest(new { Message = registerError.ErrorMessage });
            }

            return CreatedAtAction(nameof(Register), new { Message = "User created." });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/[controller]/verify")]
        public IActionResult Verify()
        {
            string cookieName = _tokenService.GetCookieName();

            if (Request.Cookies[cookieName] != null)
            {
                string? jwtToken = Request.Cookies[cookieName];

                if (jwtToken != null)
                {
                    bool valid = _authService.VerifyToken(jwtToken);

                    if (valid)
                    {
                        JwtSecurityTokenHandler handler = new();
                        JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(jwtToken);

                        return Ok(new
                        {
                            Email = jwtSecurityToken.Claims.Where(
                            claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                            .First()
                            .Value
                        });
                    }
                }
            }

            return Unauthorized(new { Message = "Token was invalid." });
        }
    }
}
