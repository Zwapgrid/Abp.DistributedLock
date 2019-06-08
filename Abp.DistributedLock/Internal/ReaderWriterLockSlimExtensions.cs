using System;
using System.Threading;

namespace Abp.Locking.Internal
{
    internal static class ReaderWriterLockSlimExtensions
    {
        private sealed class ReadLockToken : IDisposable
        {
            private ReaderWriterLockSlim _sync;
            public ReadLockToken(ReaderWriterLockSlim sync)
            {
                _sync = sync;
                sync.EnterUpgradeableReadLock();
            }
            public void Dispose()
            {
                if (_sync != null)
                {
                    if (_sync.IsUpgradeableReadLockHeld)
                    {
                        _sync.ExitUpgradeableReadLock();
                    }
                }
            }
        }
        private sealed class WriteLockToken : IDisposable
        {
            private ReaderWriterLockSlim _sync;
            public WriteLockToken(ReaderWriterLockSlim sync)
            {
                _sync = sync;
                sync.EnterWriteLock();
            }
            public void Dispose()
            {
                if (_sync != null)
                {
                    if (_sync.IsWriteLockHeld)
                    {
                        _sync.ExitWriteLock();
                    }
                }
            }
        }

        internal static IDisposable Read(this ReaderWriterLockSlim obj)
        {
            return new ReadLockToken(obj);
        }
        internal static IDisposable Write(this ReaderWriterLockSlim obj)
        {
            return new WriteLockToken(obj);
        }
    }
}
