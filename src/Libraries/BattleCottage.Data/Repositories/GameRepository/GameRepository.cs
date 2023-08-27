using BattleCottage.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BattleCottage.Data.Repositories.GameRepository
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {

            _context = context;

        }
        public Task<Game> AddAsync(Game entity)
        {
            throw new NotImplementedException();
        }

        public async Task CreateMultiple(IEnumerable<Game> games)
        {
            await _context.AddRangeAsync(games);
            await _context.SaveChangesAsync();
        }

        public Task<Game> DeleteAsync(Game entity)
        {
            throw new NotImplementedException();
        }

        public Task<Game> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateAsync(Game entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Game>?> Filter(Expression<Func<Game, bool>> filter)
        {
            IQueryable<Game> query = _context.Set<Game>();
            IList<Game> games = await query.Where(filter).ToListAsync();

            return games ?? null;
        }
    }
}
