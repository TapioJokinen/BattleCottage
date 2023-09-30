using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
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

        public async Task<ICollection<Game>?> GetAllGames()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<ICollection<Game>?> GetGamesWithNameLike(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ICollection<Game>? games = await _gameRepository.Filter(game => game.Name.ToLower().Contains(name.ToLower()));

            if (games == null || games.Count == 0)
            {
                throw new ObjectNotFoundException($"No games found with name like {name}.");
            }

            return games;
        }
    }
}
