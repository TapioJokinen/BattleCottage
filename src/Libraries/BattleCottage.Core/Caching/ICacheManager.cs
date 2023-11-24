namespace BattleCottage.Core.Caching;

public interface ICacheManager : IDisposable, ICacheKeyService
{
    Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> getData);
    Task<(bool, T?)> TryGetItemAsync<T>(string key);
    void SetLocal(string key, object? value);
}