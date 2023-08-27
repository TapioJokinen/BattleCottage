using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleCottage.Services.RAWG
{
    public class ConsumeRAWGGamesService : BackgroundService
    {
        private readonly ILogger<ConsumeRAWGGamesService> _logger;

        public ConsumeRAWGGamesService(ILogger<ConsumeRAWGGamesService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ConsumeRAWGGamesService is working.");
            await DoWork(cancellationToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

            using var scope = Services.CreateScope();
            var rawgGamesService =
                scope.ServiceProvider
                    .GetRequiredService<IRAWGGamesService>();

            await rawgGamesService.DoWork(cancellationToken);
        }
    }
}
