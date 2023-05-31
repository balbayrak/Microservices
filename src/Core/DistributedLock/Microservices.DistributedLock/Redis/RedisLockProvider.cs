using Microsoft.Extensions.Logging;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.DistributedLock.Redis
{
    public static class RedLockProvider
    {
        public static RedLockFactory RedLockFactoryObject;

        public static void SetRedLockFactory(DistributedLockOption redisOptions, ILoggerFactory loggerFactory)
        {
            if (string.IsNullOrEmpty(redisOptions.Connection))
            {
                throw new ArgumentException("Invalid RedisUrl for Creating RedLock");
            }

            var endpoints = new List<RedLockEndPoint>() {
            new RedLockEndPoint()
            {
                EndPoint = new DnsEndPoint(redisOptions.BaseUrl, redisOptions.Port),
                //Password = redisOptions.Password
            }};

            RedLockFactoryObject = RedLockFactory.Create
                      (endpoints, redisOptions.LogLockingProcess ? loggerFactory : null);
        }
    }
}
