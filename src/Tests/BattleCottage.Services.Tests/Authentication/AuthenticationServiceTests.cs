using BattleCottage.Core.Entities;
using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.Authentication;
using BattleCottage.Services.Models;
using BattleCottage.Tests;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

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
            RegisterError? registerError1 = await _authService.Register(new RegisterCredentials() { Email = "Foo" });

            if (registerError1 == null) Assert.Fail("RegisterError was null");

            Assert.Equal(ErrorMessages.InvalidEmailFormat, registerError1.ErrorMessage);

            RegisterError? registerError2 = await _authService.Register(new RegisterCredentials() { Email = "" });

            if (registerError2 == null) Assert.Fail("RegisterError was null");

            Assert.Equal(ErrorMessages.InvalidEmailFormat, registerError2.ErrorMessage);

            RegisterError? registerError3 = await _authService.Register(new RegisterCredentials() { });

            if (registerError3 == null) Assert.Fail("RegisterError was null");

            Assert.Equal(ErrorMessages.InvalidEmailFormat, registerError3.ErrorMessage);

        }

        private async Task TestPasswordErrorCode(string password, string passwordAgain, string? expectedErrorCode)
        {

            RegisterCredentials creds = new()
            {
                Email = "a@a.com",
                Password = password,
                PasswordAgain = passwordAgain
            };

            RegisterError? registerError = await _authService.Register(creds);

            if (registerError == null) Assert.Fail("RegisterError was null");

            Assert.Equal(expectedErrorCode, registerError.ErrorMessage);

        }

        [Fact]
        public async Task Register_PasswordsAreInvalid()
        {
            await TestPasswordErrorCode("", "", ErrorMessages.EmptyPassword);
            await TestPasswordErrorCode("", "foo", ErrorMessages.EmptyPassword);
            await TestPasswordErrorCode("foo", "", ErrorMessages.EmptyPassword);
            await TestPasswordErrorCode("foo123456789!", "foo123456789!", ErrorMessages.PasswordMissingUppercase);
            await TestPasswordErrorCode("FOO123456789!", "FOO123456789!", ErrorMessages.PasswordMissingLowercase);
            await TestPasswordErrorCode("FOo123456789", "FOo123456789", ErrorMessages.PasswordMissingAlphanumeric);
            await TestPasswordErrorCode("FOoooooooooo!", "FOoooooooooo!", ErrorMessages.PasswordMissingNumeric);
            await TestPasswordErrorCode("Fo0!", "Fo0!", ErrorMessages.PasswordTooShort);
            await TestPasswordErrorCode("FOO", "FOO",
                ErrorMessages.PasswordTooShort + " " +
                ErrorMessages.PasswordMissingAlphanumeric + " " +
                ErrorMessages.PasswordMissingNumeric + " " +
                ErrorMessages.PasswordMissingLowercase
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

                RegisterError? errorCode = await _authService.Register(creds);

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

            RegisterError? registerError1 = await _authService.Register(creds);

            Assert.Null(registerError1);

            RegisterError? registerError2 = await _authService.Register(creds);

            if (registerError2 == null) Assert.Fail("RegisterError was null");

            Assert.Equal(ErrorMessages.UserAlreadyExists, registerError2.ErrorMessage);

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

            RegisterError? errorCode1 = await _authService.Register(creds);

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

            RegisterError? registerError1 = await _authService.Register(creds);

            Assert.Null(registerError1);

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
