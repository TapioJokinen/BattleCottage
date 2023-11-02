using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
using BattleCottage.Data.Repositories;

namespace BattleCottage.Services.Games
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IList<Game>?> GetAllGames()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<IList<Game>?> GetGamesWithNameLike(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IList<Game>? games = await _gameRepository.Filter(
                game => game.Name != null && game.Name.ToLower().Contains(name.ToLower())
            );

            if (games == null || games.Count == 0)
            {
                throw new ObjectNotFoundException($"No games found with name like {name}.");
            }

            return games;
        }
    }
}
