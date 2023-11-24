namespace BattleCottage.Services.RAWG;

// ReSharper disable once ClassNeverInstantiated.Global
public class Result
{
    public required string Name { get; set; }

    public required string BackgroundImage { get; set; }
}

// ReSharper disable once InconsistentNaming
public class RAWGGamesResult
{
    public List<Result>? Results { get; set; }
}