namespace Microservices.DistributedLock
{
    public interface IDistributedLockManager
    {
        void Lock(string key, Action action, CancellationToken? cancellationToken = null);
        Task LockAsync(string key, Func<Task> action, CancellationToken? cancellationToken = null);
    }
}