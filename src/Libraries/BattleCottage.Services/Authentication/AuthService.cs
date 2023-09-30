using BattleCottage.Core.Entities;
using BattleCottage.Core.Utils;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Token;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BattleCottage.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        /// <summary>
        /// Attempts to authenticate a user using the provided login credentials.
        /// If the provided credentials are valid, a JSON Web Token (JWT) is generated and returned.
        /// </summary>
        /// <param name="credentials">The login credentials containing the user's email and password.</param>
        /// <returns>
        /// A JWT if the provided credentials are valid; otherwise, returns null.
        /// </returns>
        public async Task<LoginResponse?> Login(AuthCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password)) return null;

            User? user = await _userRepository.FindByEmailAsync(credentials.Email);

            if (user != null && user.Email != null && await _userRepository.CheckPasswordAsync(user, credentials.Password))
            {
                ICollection<string> userRoles = await _userRepository.GetUserRolesAsync(user);

                var authClaims = new List<Claim>()
                {
                    new(ClaimTypes.Name, user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                JwtSecurityToken accessToken = _tokenService.GetToken(authClaims);
                string refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = _tokenService.GetRefreshTokenExpiryTime();

                await _userRepository.UpdateUserAsync(user);

                return new LoginResponse()
                {
                    Email = credentials.Email,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = user.RefreshToken,
                    AccessTokenExpiration = _tokenService.GetAccessTokenExpiryTime(),
                    RefreshTokenExpiration = user.RefreshTokenExpiryTime
                };
            }

            return null;
        }


        /// <summary>
        /// Refreshes the access token using the provided token information.
        /// </summary>
        /// <param name="tokens">The token information containing the access token and refresh token.</param>
        /// <returns>
        /// A <see cref="TokenModel"/> containing the refreshed access token and its expiration,
        /// or <c>null</c> if the provided tokens are invalid or the refresh process fails.
        /// </returns>
        /// <remarks>
        /// This method attempts to refresh the access token by validating the provided access token,
        /// checking the associated user's refresh token, and generating a new access token with extended validity.
        /// If the refresh process is successful, a new <see cref="TokenModel"/> is returned with the refreshed tokens.
        /// If any validation step fails, or if the user's refresh token is invalid or expired,
        /// the method returns <c>null</c> to indicate a failed refresh attempt.
        /// </remarks>
        public async Task<TokenModel?> RefreshAccessToken(TokenModel tokens)
        {
            if (tokens == null || tokens.AccessToken == null || tokens.RefreshToken == null)
            {
                return null;
            }

            string? accessToken = tokens.AccessToken;
            string? refreshToken = tokens.RefreshToken;

            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return null;
            }

            if (principal.Identity == null || principal.Identity.Name == null)
            {
                return null;
            }

            string email = principal.Identity.Name;

            User? user = await _userRepository.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            JwtSecurityToken newAccessToken = _tokenService.GetToken(principal.Claims.ToList());

            return new TokenModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = user.RefreshToken,
                AccessTokenExpiration = _tokenService.GetAccessTokenExpiryTime(),
                RefreshTokenExpiration = user.RefreshTokenExpiryTime
            };
        }


        /// <summary>
        /// Registers a new user based on the provided registration credentials.
        /// If the registration is successful, null is returned; otherwise, a RegisterError containing the specific error message is returned.
        /// </summary>
        /// <param name="credentials">The registration credentials including email and password information.</param>
        /// <returns>
        /// Null if the registration is successful; otherwise, a RegisterError containing the error message.
        /// </returns>
        public async Task<RegisterError?> Register(RegisterCredentials credentials)
        {
            if (!Validator.ValidateEmail(credentials.Email)) return new RegisterError(ErrorMessages.InvalidEmailFormat);

            if (string.IsNullOrEmpty(credentials.Password) || string.IsNullOrEmpty(credentials.PasswordAgain))
                return new RegisterError(ErrorMessages.EmptyPassword);

            if (credentials.Password != credentials.PasswordAgain)
                return new RegisterError(ErrorMessages.PasswordsDoNotMatch);

            ICollection<string> passwordErrors = await _userRepository.ValidatePasswordAsync(credentials.Password);

            if (passwordErrors.Count > 0)
                return new RegisterError(string.Join(" ", passwordErrors.ToArray()));

            User? userExists = await _userRepository.FindByEmailAsync(credentials.Email);

            if (userExists != null)
                return new RegisterError(ErrorMessages.UserAlreadyExists);

            var user = new User()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userRepository.AddUserAsync(user, credentials.Password);

            if (result != IdentityResult.Success)
                return new RegisterError("Failed to create user. Try again later.");

            return null;
        }
    }
}
