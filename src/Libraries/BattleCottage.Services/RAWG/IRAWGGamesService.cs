namespace BattleCottage.Services.RAWG
{
    public interface IRAWGGamesService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
