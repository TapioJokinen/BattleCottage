using BattleCottage.Core.Entities;
using BattleCottage.Data.Repositories.GameRepository;

namespace BattleCottage.Services.Games
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<ICollection<Game>?> GetGamesWithNameLike(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            ICollection<Game> games = await _gameRepository.Filter(game => game.Name.ToLower().Contains(name.ToLower()));

            return games.Count == 0 ? null : games;
        }
    }
}
