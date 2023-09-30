using System.Linq.Expressions;
using BattleCottage.Core.Entities;

namespace BattleCottage.Data.Repositories.GameStyleRepository
{
    public class GameStyleRepository : IGameStyleRepository
    {
        private readonly ApplicationDbContext _context;

        public GameStyleRepository(ApplicationDbContext context)
        {

            _context = context;

        }

        public Task<GameStyle> AddAsync(GameStyle entity)
        {
            throw new NotImplementedException();
        }

        public Task<GameStyle> DeleteAsync(GameStyle entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<GameStyle>?> Filter(Expression<Func<GameStyle, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<GameStyle?> FindByIdAsync(int id)
        {
            return await _context.GameStyles.FindAsync(id);
        }

        public Task<ICollection<GameStyle>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameStyle> UpdateEntityAsync(GameStyle entity)
        {
            throw new NotImplementedException();
        }
    }
}