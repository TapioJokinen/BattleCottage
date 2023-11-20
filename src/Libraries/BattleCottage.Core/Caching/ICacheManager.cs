namespace BattleCottage.Core.Caching;

public interface ICacheManager
{
    Task<(bool, T? item)> TryGetDataAsync<T>(string key);
    Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> acquire);
    Task RemoveAsync(CacheKey key);

    void SetLocal(string key, object? data);
}