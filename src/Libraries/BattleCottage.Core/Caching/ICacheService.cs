namespace BattleCottage.Core.Caching
{
    public interface ICacheService
    {
        Task<T?> Get<T>(string key);
        Task Set<T>(string key, T value);
        Task Remove(string key);
    }
}
