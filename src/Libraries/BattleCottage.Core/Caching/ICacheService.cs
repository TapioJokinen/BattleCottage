namespace BattleCottage.Core.Caching
{
    public interface ICacheService
    {
        CacheKey GenerateDefaultCacheKey(CacheKey key, params object[] prefixes);
        string GenerateCacheKeyHash(object parameter);
    }
}
