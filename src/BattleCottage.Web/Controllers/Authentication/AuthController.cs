using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Route("/api/a/login")]
        public async Task<IActionResult> Login([FromBody] AuthCredentials loginCredentials)
        {
            var loginResponse = await _authService.Login(loginCredentials);

            if (loginResponse == null)
            {
                return Unauthorized(new MessageResponse("Invalid credentials."));
            }

            return Ok(loginResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/api/a/register")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentials registerCredentials)
        {
            await _authService.Register(registerCredentials);

            return CreatedAtAction(nameof(Register), new MessageResponse("User created."));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/a/refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokens)
        {
            var refreshedTokens = await _authService.RefreshAccessToken(tokens);

            if (refreshedTokens != null)
            {
                return Ok(refreshedTokens);
            }

            return Unauthorized(new MessageResponse("Tokens were expired or invalid."));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/a/revoke")]
        public async Task<IActionResult> Revoke()
        {
            var email = HttpContext.User.Identity?.Name;

            if (email == null)
            {
                return Unauthorized(new MessageResponse("Failed to authorize user."));
            }

            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new MessageResponse("User not found."));
            }

            user.RefreshToken = null;

            await _userRepository.UpdateUserAsync(user);

            return Ok(new MessageResponse("Revoked token successfully."));
        }
    }
}
