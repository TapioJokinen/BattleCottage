namespace BattleCottage.Services.RAWG;

// ReSharper disable once InconsistentNaming
public interface IRAWGGamesService
{
    Task DoWork(CancellationToken cancellationToken);
}