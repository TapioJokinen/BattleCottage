using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Models;
using BattleCottage.Services.Models.ConstrollerResponses;
using BattleCottage.Services.Token;
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
        [Route("/api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            LoginResponse? loginResponse = await _authService.Login(loginCredentials);

            if (loginResponse == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            return Ok(loginResponse);
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
        [Route("/api/[controller]/refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokens)
        {
            TokenModel? refreshedTokens = await _authService.RefreshAccessToken(tokens);

            if (refreshedTokens != null)
            {
                return Ok(refreshedTokens);
            }

            return Unauthorized(new { Message = "Tokens were expired or invalid." });
        }
    }
}
