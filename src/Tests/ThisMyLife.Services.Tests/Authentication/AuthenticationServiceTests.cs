using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using BattleCottage.Core.Entities;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Models;
using BattleCottage.Tests;

namespace BattleCottage.Services.Tests.Authentication
{
    public class AuthenticationServiceTests : IClassFixture<ServicesWebApplicationFactory<Program>>, IDisposable
    {
        private readonly ServicesWebApplicationFactory<Program> _factory;
        private readonly IServiceScope _scope;
        private readonly IAuthService _authService;
        private readonly IDatabaseOperations _dbOperations;

        public AuthenticationServiceTests(ServicesWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _authService = _scope.ServiceProvider.GetService<IAuthService>() ?? throw new ArgumentException("");
            _dbOperations = _scope.ServiceProvider.GetService<IDatabaseOperations>() ?? throw new ArgumentException("");

        }

        public void Dispose()
        {
            _dbOperations.TruncateDatabase();
        }

        [Fact]
        public async Task Register_EmailIsInvalid()
        {
            string? errorCode1 = await _authService.Register(new RegisterCredentials() { Email = "Foo" });

            Assert.Equal(ErrorCodes.InvalidEmailFormat, errorCode1);

            string? errorCode2 = await _authService.Register(new RegisterCredentials() { Email = "" });

            Assert.Equal(ErrorCodes.InvalidEmailFormat, errorCode2);

            string? errorCode3 = await _authService.Register(new RegisterCredentials() { });

            Assert.Equal(ErrorCodes.InvalidEmailFormat, errorCode3);

        }

        private async Task TestPasswordErrorCode(string password, string passwordAgain, string? expectedErrorCode)
        {

            RegisterCredentials creds = new()
            {
                Email = "a@a.com",
                Password = password,
                PasswordAgain = passwordAgain
            };

            string? errorCode = await _authService.Register(creds);

            Assert.Equal(expectedErrorCode, errorCode);

        }

        [Fact]
        public async Task Register_PasswordsAreInvalid()
        {
            await TestPasswordErrorCode("", "", ErrorCodes.EmptyPassword);
            await TestPasswordErrorCode("", "foo", ErrorCodes.EmptyPassword);
            await TestPasswordErrorCode("foo", "", ErrorCodes.EmptyPassword);
            await TestPasswordErrorCode("foo123456789!", "foo123456789!", ErrorCodes.PasswordMissingUppercase);
            await TestPasswordErrorCode("FOO123456789!", "FOO123456789!", ErrorCodes.PasswordMissingLowercase);
            await TestPasswordErrorCode("FOo123456789", "FOo123456789", ErrorCodes.PasswordMissingAlphanumeric);
            await TestPasswordErrorCode("FOoooooooooo!", "FOoooooooooo!", ErrorCodes.PasswordMissingNumeric);
            await TestPasswordErrorCode("Fo0!", "Fo0!", ErrorCodes.PasswordTooShort);
            await TestPasswordErrorCode("FOO", "FOO",
                ErrorCodes.PasswordTooShort + " " +
                ErrorCodes.PasswordMissingAlphanumeric + " " +
                ErrorCodes.PasswordMissingNumeric + " " +
                ErrorCodes.PasswordMissingLowercase
                );
        }

        [Fact]
        public async Task Register_PasswordsAreOK()
        {
            IUserRepository? userRepository = _scope.ServiceProvider.GetService<IUserRepository>();

            if (userRepository != null)
            {
                RegisterCredentials creds = new()
                {
                    Email = "a@a.com",
                    Password = "SuperStrongPassword123!",
                    PasswordAgain = "SuperStrongPassword123!"
                };

                string? errorCode = await _authService.Register(creds);

                Assert.Null(errorCode);

                User? user = await userRepository.FindByEmailAsync(creds.Email);

                if (user != null)
                {

                    Assert.Equal(user.Email, creds.Email);
                }
                else
                {
                    Assert.Fail("User not created.");
                }
            }
            else
            {
                Assert.Fail("AuthService not found.");
            }
        }

        [Fact]
        public async Task Register_EnsureUserAreNotCretedTwice()
        {
            RegisterCredentials creds = new()
            {
                Email = "a@a.com",
                Password = "SuperStrongPassword123!",
                PasswordAgain = "SuperStrongPassword123!"
            };

            string? errorCode1 = await _authService.Register(creds);

            Assert.Null(errorCode1);

            string? errorCode2 = await _authService.Register(creds);

            Assert.Equal(ErrorCodes.UserAlreadyExists, errorCode2);

        }

        [Fact]
        public async Task Login_EnsureTokenIsReturnedWhenCorrectCreds()
        {
            RegisterCredentials creds = new()
            {
                Email = "a@a.com",
                Password = "SuperStrongPassword123!",
                PasswordAgain = "SuperStrongPassword123!"
            };

            string? errorCode1 = await _authService.Register(creds);

            Assert.Null(errorCode1);

            LoginCredentials creds2 = new()
            {
                Email = "a@a.com",
                Password = "SuperStrongPassword123!"
            };

            JwtSecurityToken? token = await _authService.Login(creds2);

            Assert.NotNull(token);
        }

        [Fact]
        public async Task Login_EnsureTokenIsNotReturnedWhenInCorrectCreds()
        {
            RegisterCredentials creds = new()
            {
                Email = "a@a.com",
                Password = "SuperStrongPassword123!",
                PasswordAgain = "SuperStrongPassword123!"
            };

            string? errorCode1 = await _authService.Register(creds);

            Assert.Null(errorCode1);

            LoginCredentials creds2 = new()
            {
                Email = "a@a.com",
                Password = "InvalidPassword123!"
            };

            JwtSecurityToken? token = await _authService.Login(creds2);

            Assert.Null(token);
        }
    }
}
