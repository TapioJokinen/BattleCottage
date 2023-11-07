namespace BattleCottage.Core.Caching
{
    public interface ICacheManager : IDisposable
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
        Task RemoveAsync(string key);
        CacheKey GenerateDefaultCacheKey(CacheKey key, params object[] prefixes);
    }
}
