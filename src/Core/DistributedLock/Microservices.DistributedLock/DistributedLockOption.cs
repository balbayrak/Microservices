using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DistributedLock
{
    public class DistributedLockOption
    {
        public DistributedLockTypes DistributedLockType { get; set; }
        public string BaseUrl { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Connection => $"{BaseUrl}:{Port}";
        public int ExpiryTimeFromSeconds { get; set; }
        public int WaitTimeFromSeconds { get; set; }
        public int RetryTimeFromMilliseconds { get; set; }
        public bool LogLockingProcess { get; set; }
    }
}
