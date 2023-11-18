namespace BattleCottage.Core.Caching
{
    /// <summary>
    /// Represents a cache key used to store and retrieve cached data.
    /// </summary>
    public class CacheKey
    {
        public CacheKey(string key)
        {
            Key = key;
        }

        public string Key { get; set; }
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.FromMinutes(60);
        public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(10);
    }
}
