using System.Linq.Expressions;

namespace BattleCottage.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IList<T>?> Filter(Expression<Func<T, bool>> filter);

        Task<T> FindByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
