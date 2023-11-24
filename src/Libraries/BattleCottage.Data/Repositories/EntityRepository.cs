using System.Linq.Expressions;
using BattleCottage.Core.Caching;
using BattleCottage.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleCottage.Data.Repositories;

public class EntityRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ICacheManager _cacheManager;
    private readonly ApplicationDbContext _context;

    public EntityRepository(ApplicationDbContext context, ICacheManager cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    /// <summary>
    ///     Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    /// <summary>
    ///     Deletes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deleted entity.</returns>
    public Task<TEntity> DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Filters the entities in the database based on the given filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply to the entities.</param>
    /// <returns>A list of entities that match the filter expression, or null if no entities match.</returns>
    public async Task<IList<TEntity>?> Filter(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> dbSet = _context.Set<TEntity>();
        IList<TEntity> entities = await dbSet.Where(filter).ToListAsync();

        return entities.Count == 0 ? null : entities;
    }

    /// <summary>
    ///     Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <returns>A list of all entities of type TEntity, or null if none are found.</returns>
    public async Task<IList<TEntity>?> GetAllAsync()
    {
        var cacheKey = _cacheManager.PrepareCacheKey(CacheDefaults<TEntity>.AllValuesCacheKey);

        return await _cacheManager.GetAsync(cacheKey, GetAllEntities);

        async Task<IList<TEntity>?> GetAllEntities()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }

    /// <summary>
    ///     Finds an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <returns>The entity with the specified ID, or null if not found.</returns>
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var cacheKey = _cacheManager.PrepareCacheKey(CacheDefaults<TEntity>.ByIdCacheKey, id);

        return await _cacheManager.GetAsync(cacheKey, GetEntity);

        async Task<TEntity?> GetEntity()
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }

    public Task<IList<TEntity>?> GetByIdsAsync(params int[] ids)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Updates an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Saves all changes made in this context to the underlying database asynchronously.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains the number of state entries
    ///     written to the database.
    /// </returns>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Adds a range of entities to the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <returns>A list of added entities.</returns>
    public async Task<IList<TEntity>?> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var baseEntities = entities.ToList();
        await _context.Set<TEntity>().AddRangeAsync(baseEntities);
        return baseEntities.ToList();
    }

    /// <summary>
    ///     Retrieves entities by ID's asynchronously.
    /// </summary>
    /// <param name="ids">The IDs of the entities to retrieve.</param>
    /// <returns>The retrieved entities, or null if they do not exist.</returns>
    public Task<IList<TEntity>?> GetByIdsAsync(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}