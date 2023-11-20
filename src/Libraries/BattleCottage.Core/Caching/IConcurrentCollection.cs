namespace BattleCottage.Core.Caching;

public interface IConcurrentCollection<TValue>
{
    void Add(string key, TValue value);
    void Remove(string key);
    bool TryGetValue(string key, out TValue value);
}