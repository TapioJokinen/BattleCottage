using BattleCottage.Data;
using BattleCottage.Services.Models.ConstrollerResponses;

namespace BattleCottage.Services.HealthCheck
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly ApplicationDbContext _context;

        public HealthCheckService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> HealthCheck()
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            var response = new HealthCheckResult()
            {
                BackendStatus = HealthCheckStatus.Healthy.ToString(),
                DatabaseStatus = canConnect
                    ? HealthCheckStatus.Healthy.ToString()
                    : HealthCheckStatus.NotHealthy.ToString(),
                CurrentTime = DateTime.UtcNow,
            };

            return response;
        }
    }
}
