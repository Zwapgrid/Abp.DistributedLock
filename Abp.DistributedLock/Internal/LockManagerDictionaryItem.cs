using Nito.AsyncEx;

namespace Abp.Locking.Internal
{
    internal class LockManagerDictionaryItem
    {
        public AsyncLock Lock { get; set; }
        public int Counter { get; set; }
    }
}
