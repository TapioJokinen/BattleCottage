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
    }
}
