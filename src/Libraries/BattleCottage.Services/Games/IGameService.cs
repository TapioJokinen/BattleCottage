using BattleCottage.Core.Entities;

namespace BattleCottage.Services.Games
{
    public interface IGameService
    {
        Task<ICollection<Game>?> GetGamesWithNameLike(string? name);
    }
}
