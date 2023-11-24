namespace BattleCottage.Core.Infrastructure;

public class ConcurrentTrie<TValue> : IConcurrentTrie<TValue>
{
    // The locking could be fine-tuned to allow for more concurrency with lock striping.
    // Unfortunately I'm too lazy (too bad) at the moment to do it.

    private readonly TrieNode _root = new();
    private readonly ReaderWriterLockSlim _structureLock = new();

    public void Add(string key, TValue? value)
    {
        _structureLock.EnterWriteLock();
        try
        {
            var node = _root;

            foreach (var letter in key)
            {
                node.Children.TryAdd(letter, new TrieNode());
                node = node.Children[letter];
            }

            node.IsWord = true;
            node.Value = value;
        }
        finally
        {
            _structureLock.ExitWriteLock();
        }
    }

    public bool TryGetValue(string key, out TValue? value)
    {
        _structureLock.EnterReadLock();
        try
        {
            var node = _root;
            value = default!;

            foreach (var letter in key)
            {
                if (!node.Children.TryGetValue(letter, out var _)) return false;

                node = node.Children[letter];
            }

            if (!node.IsWord) return false;

            value = node.Value;
            return true;
        }
        finally
        {
            _structureLock.ExitReadLock();
        }
    }

    public void Remove(string key)
    {
        _structureLock.EnterReadLock();
        try
        {
            var node = _root;

            // Traverse the trie to the node that represents the key
            var nodesStack = new Stack<TrieNode>();

            foreach (var letter in key)
            {
                nodesStack.Push(node);
                node = node.Children[letter];
            }

            // Mark the node as not a word
            node.IsWord = false;
            node.Value = default!;

            // Remove the node if it has no children
            if (node.Children.Count != 0 || nodesStack.Count <= 0) return;

            var parent = nodesStack.Pop();
            parent.Children.Remove(key[^1]);
        }
        finally
        {
            _structureLock.ExitReadLock();
        }
    }

    private class TrieNode
    {
        public TValue? Value { get; set; }
        public bool IsWord { get; set; }
        public Dictionary<char, TrieNode> Children { get; } = new();
    }
}