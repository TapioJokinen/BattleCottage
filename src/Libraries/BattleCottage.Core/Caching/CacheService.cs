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

        /// <summary>
        /// Gets the value associated with the specified key from the cache.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or the default value of <typeparamref name="T"/> if the key is not found in the cache.</returns>
        public async Task<T?> Get<T>(string key)
        {
            byte[]? value = await _cache.GetAsync(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value));
            }
            return default;
        }

        /// <summary>
        /// Removes the cached item with the specified key.
        /// </summary>
        /// <param name="key">The key of the cached item to remove.</param>
        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// Sets a value in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value to be cached.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to be cached.</param>
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