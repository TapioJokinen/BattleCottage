namespace BattleCottage.Core.Caching;

public class CacheKey
{
    public CacheKey(string key, string prefix)
    {
        Prefix = prefix;
        Key = $"{Prefix}:{Key}";
    }

    public string Key { get; }
    public string Prefix { get; }

    public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(60);
}