using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BattleCottage.Core.Caching;

public class DistributedCacheManager : ICacheManager
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConcurrentCollection<object> _localCache;
    private readonly ICacheKeyManager _localKeyManager;

    public DistributedCacheManager(ICacheKeyManager localKeyManager, IDistributedCache distributedCache,
        IConcurrentCollection<object> localCache)
    {
        _localKeyManager = localKeyManager;
        _distributedCache = distributedCache;
        _localCache = localCache;
    }

    public Task RemoveAsync(CacheKey key)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool, T? item)> TryGetDataAsync<T>(string key)
    {
        var jsonData = await _distributedCache.GetStringAsync(key);

        return string.IsNullOrEmpty(jsonData)
            ? (false, default)
            : (true, item: JsonConvert.DeserializeObject<T>(jsonData));
    }

    public async Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> acquire)
    {
        if (_localCache.TryGetValue(key.Key, out var localItem)) return (T?)localItem;

        var (found, item) = await TryGetDataAsync<T>(key.Key);

        if (found) return item;

        var data = await acquire();

        await _distributedCache.SetStringAsync(key.Key, JsonConvert.SerializeObject(data),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = key.Expiration
            });

        return data;
    }

    public void SetLocal(string key, object data)
    {
        _localCache.Add(key, data);
        _localKeyManager.AddKey(key);
    }

    public Task SetAsync(CacheKey key, object data)
    {
        throw new NotImplementedException();
    }
}