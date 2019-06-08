using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.Locking
{
    // TODO add documentation
    public interface ILockManager
    {
        void PerformInLock(string key, Action actionTodo);
        void PerformInLock(string key, Action actionTodo, TimeSpan? expireIn = null);
        void PerformInLock(string key, Action actionTodo, CancellationToken cancellationToken = default);
        TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo);
        TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo, TimeSpan? expireIn = null);
        TResult PerformInLock<TResult>(string key, Func<TResult> actionTodo, CancellationToken cancellationToken = default);
        Task PerformInLockAsync(string key, Func<Task> actionTodo);
        Task PerformInLockAsync(string key, Func<Task> actionTodo, TimeSpan? expireIn = null);
        Task PerformInLockAsync(string key, Func<Task> actionTodo, CancellationToken cancellationToken = default);
        Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo);
        Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo, TimeSpan? expireIn = null);
        Task<TResult> PerformInLockAsync<TResult>(string key, Func<Task<TResult>> actionTodo, CancellationToken cancellationToken = default);
    }
}
