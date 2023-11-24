using BattleCottage.Core.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BattleCottage.Core.Caching;

public class CacheManager : CacheKeyService, ICacheManager
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConcurrentTrie<object> _localCache;

    public CacheManager(IDistributedCache distributedCache, IConcurrentTrie<object> localCache)
    {
        _distributedCache = distributedCache;
        _localCache = localCache;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> getData)
    {
        if (_localCache.TryGetValue(key.Key, out var value))
            return (T?)value;

        var (isSet, item) = await TryGetItemAsync<T>(key.Key);

        if (!isSet || item == null)
        {
            item = await getData();

            await _distributedCache.SetStringAsync(key.Key, JsonConvert.SerializeObject(item),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = key.AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = key.SlidingExpiration
                });
        }

        SetLocal(key.Key, item);

        return item;
    }

    public void SetLocal(string key, object? value)
    {
        _localCache.Add(key, value);
    }

    private async Task<(bool isSet, T? item)> TryGetItemAsync<T>(string key)
    {
        var dataAsJson = await _distributedCache.GetStringAsync(key);

        return string.IsNullOrEmpty(dataAsJson)
            ? (false, default)
            : (true, JsonConvert.DeserializeObject<T>(dataAsJson));
    }
}