using RedLockNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DistributedLock.Redis
{
    public class RedisDistributedLockManager : IDistributedLockManager
    {
        private readonly IDistributedLockFactory _distributedLockFactory;
        private readonly DistributedLockOption _distributedLockOption;

        public RedisDistributedLockManager(IDistributedLockFactory distributedLockFactory, DistributedLockOption distributedLockOption)
        {
            _distributedLockFactory = distributedLockFactory;
            _distributedLockOption = distributedLockOption;
        }
        public void Lock(string key, Action action, CancellationToken? cancellationToken = null)
        {
            using (var redLock = _distributedLockFactory.CreateLock(key, TimeSpan.FromSeconds(_distributedLockOption.ExpiryTimeFromSeconds),
                TimeSpan.FromSeconds(_distributedLockOption.WaitTimeFromSeconds),
                TimeSpan.FromSeconds(_distributedLockOption.RetryTimeFromMilliseconds), cancellationToken
                ))
            {

                if (redLock.IsAcquired)
                {
                    action();
                }
            }
        }

        public async Task LockAsync(string key, Func<Task> action, CancellationToken? cancellationToken = null)
        {
            await using (var redLock = await _distributedLockFactory.CreateLockAsync(key, TimeSpan.FromSeconds(_distributedLockOption.ExpiryTimeFromSeconds),
            TimeSpan.FromSeconds(_distributedLockOption.WaitTimeFromSeconds),
            TimeSpan.FromSeconds(_distributedLockOption.RetryTimeFromMilliseconds), cancellationToken
            ))
            {
                if (redLock.IsAcquired)
                {
                    await action();
                }
            }
        }
    }
}
