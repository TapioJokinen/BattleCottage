namespace BattleCottage.Core.Caching
{
    public class RedisCacheManager : CacheService, ICacheManager
    {
        public Task<T?> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
