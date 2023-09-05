using BattleCottage.Services.Authentication;
using BattleCottage.Tests;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
