namespace BattleCottage.Services.Models
{
    public class Result
    {
        public required string Name { get; set; }

        public required string BackgroundImage { get; set; }
    }

    public class RAWGGamesResponse
    {
        public List<Result>? Results { get; set; }
    }
}
