namespace BattleCottage.Core.Caching;

public class CacheKey
{
    public TimeSpan AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
    public TimeSpan SlidingExpiration = TimeSpan.FromMinutes(1);

    public CacheKey(string key, params string[] prefixes)
    {
        Key = key;
        Prefixes.AddRange(prefixes.Where(p => !string.IsNullOrEmpty(p)));
    }

    public string Key { get; private set; }
    public List<string> Prefixes { get; set; } = new();

    public CacheKey Create(string keyParameter)
    {
        var cacheKey = new CacheKey(Key, Prefixes.ToArray());

        cacheKey.Key = string.Format(cacheKey.Key, keyParameter);

        for (var i = 0; i < cacheKey.Prefixes.Count; i++)
            cacheKey.Prefixes[i] = string.Format(cacheKey.Prefixes[i], keyParameter);

        return cacheKey;
    }
}