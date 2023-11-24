using BattleCottage.Core.Utils;

namespace BattleCottage.Core.Caching;

public class CacheKeyService : ICacheKeyService
{
    public CacheKey PrepareCacheKey(CacheKey key, params int[] ids)
    {
        var cacheKey = key.Create(CreateHashForIds(ids));

        return cacheKey;
    }

    private static string CreateHashForIds(params int[] ids)
    {
        var sortedIdsAsString = string.Join(",", ids.OrderBy(i => i).ToArray());
        return Hash.GetSha256Hash(sortedIdsAsString);
    }
}