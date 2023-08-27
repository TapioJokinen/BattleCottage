using BattleCottage.Core.Entities;

namespace BattleCottage.Data.Repositories.GameRepository
{
    public interface IGameRepository : IRepository<Game>
    {
        Task CreateMultiple(IEnumerable<Game> games);
    }
}
