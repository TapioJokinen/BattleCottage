using BattleCottage.Core.Utils;

namespace BattleCottage.Core.Caching
{
    public abstract class CacheService : ICacheService
    {
        public string GenerateCacheKeyHash(object parameter)
        {
            return parameter switch
            {
                null => "",
                int id => id.ToString(),
                IEnumerable<int> ids => HashHelper.GenerateSHA265Hash(string.Join(",", ids.OrderBy(id => id))),
                _ => parameter.ToString() ?? string.Empty,
            };
        }

        /// <summary>
        /// Generates a default cache key based on the provided key and prefixes.
        /// </summary>
        /// <param name="key">The base cache key.</param>
        /// <param name="prefixes">The prefixes to add to the cache key.</param>
        /// <returns>The generated cache key.</returns>
        public virtual CacheKey GenerateDefaultCacheKey(CacheKey key, params object[] prefixes)
        {
            key.Key = string.Format(key.Key, prefixes.Select(GenerateCacheKeyHash).ToArray());
            return key;
        }
    }
}
