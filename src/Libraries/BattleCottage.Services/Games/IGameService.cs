using BattleCottage.Core.Entities;

namespace BattleCottage.Services.Games
{
    public interface IGameService
    {
        Task<IList<Game>?> GetGamesWithNameLike(string? name);
        Task<IList<Game>?> GetAllGames();
    }
}
