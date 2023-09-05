using System.Linq.Expressions;

namespace BattleCottage.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();

        Task<ICollection<T>> Filter(Expression<Func<T, bool>> filter);

        Task<T> FindByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateEntityAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
