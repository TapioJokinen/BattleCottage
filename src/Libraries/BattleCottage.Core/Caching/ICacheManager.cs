namespace BattleCottage.Core.Caching;

public interface ICacheManager : IDisposable, ICacheKeyService
{
    Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> getData);
    void SetLocal(string key, object value);
}