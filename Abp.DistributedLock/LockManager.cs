using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using Nito.AsyncEx;

namespace Abp.Locking
{
    public class LockManager : ILockManager, ISingletonDependency
    {
        ConcurrentDictionary<string, AsyncLock> _locksStorage = new ConcurrentDictionary<string, AsyncLock>();
        public LockManager() { }

        public void PerformInLock(string key, Action actionTodo)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            PerformInLock(key, actionTodo, CancellationToken.None);
        }

        public void PerformInLock(string key, Action actionTodo, TimeSpan? expireIn = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            CancellationToken ct = default;
            if (expireIn.HasValue)
            {
                var cts = new CancellationTokenSource(expireIn.Value);
                ct = cts.Token;
            }
            PerformInLock(key, actionTodo, ct);
        }

        public void PerformInLock(string key, Action actionTodo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            var _lock = _locksStorage.GetOrAdd(key, (keyStr) => new AsyncLock());
            try
            {
                using (var locking = _lock.Lock(cancellationToken))
                {
                    actionTodo();
                }
            }
            finally
            {
                _locksStorage.TryRemove(key, out _);
            }
        }

        public TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            return PerformInLock(key, actionTodo, CancellationToken.None);
        }

        public TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo, TimeSpan? expireIn = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            CancellationToken ct = default;
            if (expireIn.HasValue)
            {
                var cts = new CancellationTokenSource(expireIn.Value);
                ct = cts.Token;
            }
            return PerformInLock(key, actionTodo, ct);
        }

        public TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            var _lock = _locksStorage.GetOrAdd(key, (keyStr) => new AsyncLock());
            try
            {
                using (var locking = _lock.Lock(cancellationToken))
                {
                    return actionTodo();
                }
            }
            finally
            {
                _locksStorage.TryRemove(key, out _);
            }
        }

        public Task PerformInLockAsync(string key, Func<Task> actionTodo)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            return PerformInLockAsync(key, actionTodo, CancellationToken.None);
        }

        public Task PerformInLockAsync(string key, Func<Task> actionTodo, TimeSpan? expireIn = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            CancellationToken ct = default;
            if (expireIn.HasValue)
            {
                var cts = new CancellationTokenSource(expireIn.Value);
                ct = cts.Token;
            }
            return PerformInLockAsync(key, actionTodo, ct);
        }

        public async Task PerformInLockAsync(string key, Func<Task> actionTodo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            var _lock = _locksStorage.GetOrAdd(key, (keyStr) => new AsyncLock());
            try
            {
                using (var locking = await _lock.LockAsync(cancellationToken))
                {
                    await actionTodo();
                }
            }
            finally
            {
                _locksStorage.TryRemove(key, out _);
            }
        }

        public Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            return PerformInLockAsync(key, actionTodo, CancellationToken.None);
        }

        public Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo, TimeSpan? expireIn = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            CancellationToken ct = default;
            if (expireIn.HasValue)
            {
                var cts = new CancellationTokenSource(expireIn.Value);
                ct = cts.Token;
            }

            return PerformInLockAsync(key, actionTodo, ct);
        }

        public async Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (actionTodo == null)
                throw new ArgumentNullException(nameof(actionTodo));

            var _lock = _locksStorage.GetOrAdd(key, (keyStr) => new AsyncLock());
            try
            {
                using (var locking = await _lock.LockAsync(cancellationToken))
                {
                    return await actionTodo();
                }
            }
            finally
            {
                _locksStorage.TryRemove(key, out _);
            }
        }
    }
}
