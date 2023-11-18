using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BattleCottage.Core.Caching
{
    // TODO: Implement DistributedCacheManager instead of this?
    public class RedisCacheManager : CacheService, ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(CacheKey key, Func<Task<T>> query)
        {
            var jsonData = await _distributedCache.GetStringAsync(key.Key);

            if (string.IsNullOrEmpty(jsonData))
            {
                var data = await query();

                await SetAsync(key, data);

                return data;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task SetAsync<T>(CacheKey key, T data)
        {
            if (data == null)
            {
                return;
            }

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = key.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = key.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data);
            await _distributedCache.SetStringAsync(key.Key, jsonData, options);
        }

        public Task RemoveAsync(CacheKey key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
