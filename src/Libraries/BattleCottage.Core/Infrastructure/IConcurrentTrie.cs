namespace BattleCottage.Core.Infrastructure;

public interface IConcurrentTrie<TValue>
{
    void Add(string key, TValue? value);
    bool TryGetValue(string key, out TValue? value);
    void Remove(string key);
}