namespace BattleCottage.Core.Caching
{
    public interface ICacheManager : IDisposable
    {
        Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> query);
        Task SetAsync<T>(CacheKey key, T value);
        Task RemoveAsync(CacheKey key);
        CacheKey GenerateDefaultCacheKey(CacheKey key, params object[] prefixes);
    }
}
