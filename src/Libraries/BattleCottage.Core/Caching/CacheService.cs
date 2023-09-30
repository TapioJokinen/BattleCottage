using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BattleCottage.Core.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> Get<T>(string key)
        {
            byte[]? value = await _cache.GetAsync(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value));
            }
            return default;
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task Set<T>(string key, T value)
        {
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)

            });
        }
    }
}