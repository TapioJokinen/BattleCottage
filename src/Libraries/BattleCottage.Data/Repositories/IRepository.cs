using System.Linq.Expressions;

namespace BattleCottage.Data.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<IList<TEntity>?> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<TEntity?> FindByIdAsync(int id);

        Task<TEntity> UpdateEntityAsync(TEntity entity);

        Task<IList<TEntity>?> GetAllAsync();

        Task<IList<TEntity>?> Filter(Expression<Func<TEntity, bool>> filter);

        Task<int> SaveChangesAsync();
    }
}
