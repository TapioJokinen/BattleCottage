using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
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

        public async Task<LoginResponse> Login(AuthCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }
            ;

            User? user = await _userRepository.FindByEmailAsync(credentials.Email);

            if (
                user != null
                && user.Email != null
                && await _userRepository.CheckPasswordAsync(user, credentials.Password)
            )
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

            throw new ArgumentException("Invalid credentials.");
        }

        public async Task<TokenModel> RefreshAccessToken(TokenModel tokens)
        {
            if (tokens == null || tokens.AccessToken == null || tokens.RefreshToken == null)
            {
                throw new TokenException("Invalid token.");
            }

            string? accessToken = tokens.AccessToken;
            string? refreshToken = tokens.RefreshToken;

            ClaimsPrincipal? principal =
                _tokenService.GetPrincipalFromExpiredToken(accessToken) ?? throw new TokenException("Invalid token.");

            if (principal.Identity == null || principal.Identity.Name == null)
            {
                throw new TokenException("Invalid token.");
            }

            string email = principal.Identity.Name;

            User? user = await _userRepository.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new TokenException("Invalid token.");
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

        public async Task Register(RegisterCredentials credentials)
        {
            if (!Validator.ValidateEmail(credentials.Email))
                throw new RegisterException("The provided email was not in correct format.");

            if (string.IsNullOrEmpty(credentials.Password) || string.IsNullOrEmpty(credentials.PasswordAgain))
                throw new RegisterException("Password must not be empty.");

            if (credentials.Password != credentials.PasswordAgain)
                throw new RegisterException("Passwords do not match.");

            User? userExists = await _userRepository.FindByEmailAsync(credentials.Email);

            if (userExists != null)
                throw new RegisterException("A user with this email already exists.");

            var user = new User()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userRepository.AddUserAsync(user, credentials.Password);

            if (result != IdentityResult.Success)
                throw new RegisterException(string.Join(" ", result.Errors.Select(x => x.Description)));
        }
    }
}
