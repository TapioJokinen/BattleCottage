namespace BattleCottage.Core.Caching;

public class CacheKeyManager : ICacheKeyManager
{
    // This is type byte because we don't need to store any values.
    private readonly IConcurrentCollection<byte> _keys;

    public CacheKeyManager(IConcurrentCollection<byte> keys)
    {
        _keys = keys;
    }

    public void AddKey(string key)
    {
        _keys.Add(key, default);
    }

    public void RemoveKey(string key)
    {
        _keys.Remove(key);
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> RemoveByPrefix(string prefix)
    {
        throw new NotImplementedException();
    }
}