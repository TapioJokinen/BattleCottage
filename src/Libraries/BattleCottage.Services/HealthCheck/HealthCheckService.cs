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

        /// <summary>
        /// Returns information of the backend and database.
        /// </summary>
        /// <returns></returns>
        public async Task<HealthCheckResult> HealthCheck()
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            HealthCheckResult response = new()
            {
                BackendStatus = HealthCheckStatus.Healthy.ToString(),
                DatabaseStatus = canConnect ? HealthCheckStatus.Healthy.ToString() : HealthCheckStatus.NotHealthy.ToString(),
                CurrentTime = DateTime.UtcNow,
            };

            return response;
        }
    }
}
