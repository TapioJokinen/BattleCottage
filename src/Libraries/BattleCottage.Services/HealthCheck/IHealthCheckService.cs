using BattleCottage.Services.Models.ConstrollerResponses;

namespace BattleCottage.Services.HealthCheck
{
    public interface IHealthCheckService
    {
        public Task<HealthCheckResult> HealthCheck();
    }
}
