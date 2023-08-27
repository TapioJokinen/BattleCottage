using BattleCottage.Core.Entities;
using BattleCottage.Core.Utils;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Models;
using BattleCottage.Services.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BattleCottage.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, SignInManager<User> signInManager)
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
        public async Task<JwtSecurityToken?> Login(LoginCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Password)) return null;

            User? user = await _userRepository.FindByEmailAsync(credentials.Email);

            if (user != null && user.Email != null && await _userRepository.CheckPasswordAsync(user, credentials.Password))
            {
                IList<string> userRoles = await _userRepository.GetUserRolesAsync(user);

                List<Claim> authClaims = new()
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                JwtSecurityToken token = _tokenService.GetToken(authClaims);

                return token;
            }

            return null;
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

            IList<string> passwordErrors = await _userRepository.ValidatePasswordAsync(credentials.Password);

            if (passwordErrors.Count > 0)
                return new RegisterError(string.Join(" ", passwordErrors.ToArray()));

            User? userExists = await _userRepository.FindByEmailAsync(credentials.Email);

            if (userExists != null)
                return new RegisterError(ErrorMessages.UserAlreadyExists);

            User user = new()
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

        /// <summary>
        /// Verifies the validity of a given JSON Web Token (JWT).
        /// </summary>
        /// <param name="token">The token string to be verified.</param>
        /// <returns>
        /// True if the token is valid; otherwise, false.
        /// </returns>
        public bool VerifyToken(string token)
        {
            JwtSecurityTokenHandler handler = new();

            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = _tokenService.GetIssuer(),
                ValidAudience = _tokenService.GetAudience(),
                IssuerSigningKey = _tokenService.GetSymmetricSecurityKey(),
            };

            try
            {
                handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
