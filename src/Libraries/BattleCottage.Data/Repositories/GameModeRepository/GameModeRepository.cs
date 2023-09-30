using System.Linq.Expressions;
using BattleCottage.Core.Entities;

namespace BattleCottage.Data.Repositories.GameModeRepository
{
    public class GameModeRepository : IGameModeRepository
    {
        private readonly ApplicationDbContext _context;

        public GameModeRepository(ApplicationDbContext context)
        {

            _context = context;

        }
        public Task<GameMode> AddAsync(GameMode entity)
        {
            throw new NotImplementedException();
        }

        public Task<GameMode> DeleteAsync(GameMode entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<GameMode>?> Filter(Expression<Func<GameMode, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<GameMode?> FindByIdAsync(int id)
        {
            return await _context.GameModes.FindAsync(id);
        }

        public Task<ICollection<GameMode>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameMode> UpdateEntityAsync(GameMode entity)
        {
            throw new NotImplementedException();
        }
    }
}