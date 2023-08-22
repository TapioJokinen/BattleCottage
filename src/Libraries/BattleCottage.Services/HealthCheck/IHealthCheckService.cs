using BattleCottage.Services.Models;

namespace BattleCottage.Services.HealthCheck
{
    public interface IHealthCheckService
    {
        public Task<HealthCheckResponse> HealthCheck();
    }
}
