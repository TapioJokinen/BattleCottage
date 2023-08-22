using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BattleCottage.Core.Entities;
using BattleCottage.Core.Utils;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Models;

namespace BattleCottage.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _signInManager = signInManager;

        }

        /// <summary>
        /// Validates credentials and returns <see cref="JwtSecurityToken"/> if everything was ok.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
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
        /// Validates the registering credentials and creates a user.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>A string indicating the error. On success returns null.</returns>
        public async Task<string?> Register(RegisterCredentials credentials)
        {
            if (!Validator.ValidateEmail(credentials.Email)) return ErrorCodes.InvalidEmailFormat;

            if (string.IsNullOrEmpty(credentials.Password) || string.IsNullOrEmpty(credentials.PasswordAgain)) return ErrorCodes.EmptyPassword;

            if (credentials.Password != credentials.PasswordAgain) return ErrorCodes.PasswordsDoNotMatch;

            IList<string> passwordErrors = await _userRepository.ValidatePasswordAsync(credentials.Password);

            if (passwordErrors.Count > 0) return string.Join(" ", passwordErrors.ToArray());

            User? userExists = await _userRepository.FindByEmailAsync(credentials.Email);

            if (userExists != null) return ErrorCodes.UserAlreadyExists;

            User user = new()
            {
                Email = credentials.Email,
                UserName = credentials.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userRepository.AddUserAsync(user, credentials.Password);

            if (result != IdentityResult.Success) return "Failed to create user. Try again later.";

            return null;
        }
    }
}
