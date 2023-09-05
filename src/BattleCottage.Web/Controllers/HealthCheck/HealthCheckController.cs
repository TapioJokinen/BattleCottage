using BattleCottage.Services.HealthCheck;
using BattleCottage.Services.Models.ConstrollerResponses;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.Controllers.HealthCheck
{
    public class HealthCheckController : APIControllerBase
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        [Produces(typeof(HealthCheckResult))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("/api/[controller]")]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok(await _healthCheckService.HealthCheck());
        }
    }
}
