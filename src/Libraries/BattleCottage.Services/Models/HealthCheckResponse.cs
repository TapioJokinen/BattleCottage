namespace BattleCottage.Services.Models
{
    public class HealthCheckResponse
    {
        public required string BackendStatus { get; set; }
        public required string DatabaseStatus { get; set; }
        public required DateTime CurrentTime { get; set; }

    }
}
