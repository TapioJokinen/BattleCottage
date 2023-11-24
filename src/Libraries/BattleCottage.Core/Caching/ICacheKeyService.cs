namespace BattleCottage.Core.Caching;

public interface ICacheKeyService
{
    CacheKey PrepareCacheKey(CacheKey key, params int[] ids);
}