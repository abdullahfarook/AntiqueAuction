using System.Collections.Concurrent;

namespace AntiqueAuction.Shared.Threading
{
    public class MultiObjectLocker<TKey>
    {
        private readonly ConcurrentDictionary<TKey, object> _multiLocker = new ConcurrentDictionary<TKey, object>();
        public object Enter(TKey key)
        => _multiLocker.GetOrAdd(key, tKey => new object());
        public void Release(TKey key)
        => _multiLocker.TryRemove(key, out _);
    }
}
