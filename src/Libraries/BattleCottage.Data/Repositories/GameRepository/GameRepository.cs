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

        public Task<ICollection<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateEntityAsync(Game entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Game>> Filter(Expression<Func<Game, bool>> filter)
        {
            IQueryable<Game> query = _context.Set<Game>();
            ICollection<Game> games = await query.Where(filter).ToListAsync();

            return games;
        }
    }
}
