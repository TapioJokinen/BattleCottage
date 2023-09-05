namespace BattleCottage.Services.RAWG
{
    public class Result
    {
        public required string Name { get; set; }

        public required string BackgroundImage { get; set; }
    }

    public class RAWGGamesResult
    {
        public List<Result>? Results { get; set; }
    }
}
