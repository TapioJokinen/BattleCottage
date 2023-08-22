using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Models;

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
        [Route("/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginDto)
        {
            JwtSecurityToken? token = await _authService.Login(loginDto);

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

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/[controller]/register")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentials registerDto)
        {
            string? errorMessage = await _authService.Register(registerDto);

            if (errorMessage != null)
            {
                return BadRequest(new { Message = errorMessage });
            }

            return CreatedAtAction(nameof(Register), new { Message = "User created." });
        }
    }
}
