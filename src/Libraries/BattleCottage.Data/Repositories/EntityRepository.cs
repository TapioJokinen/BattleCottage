using System.Linq.Expressions;
using BattleCottage.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleCottage.Data.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Filters the entities in the repository based on the given filter expression.
        /// </summary>
        /// <param name="filter">The filter expression to apply to the entities.</param>
        /// <returns>A list of entities that match the filter expression, or null if no entities match.</returns>
        public async Task<IList<TEntity>?> Filter(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> dbSet = _context.Set<TEntity>();
            IList<TEntity> entities = await dbSet.Where(filter).ToListAsync();

            return entities.Count == 0 ? null : entities;
        }

        public async Task<IList<TEntity>?> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A list of added entities.</returns>
        public async Task<IList<TEntity>?> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities.ToList();
        }
    }
}