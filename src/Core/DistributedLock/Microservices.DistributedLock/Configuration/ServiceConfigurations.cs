using Microservices.DistributedLock.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedLockNet;
using StackExchange.Redis;

namespace Microservices.DistributedLock.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddDistributionLockConfigurations(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory = null)
        {
            var distributedLockOption = configuration.GetSection("DistributedLockOption").Get<DistributedLockOption>();

            services.Configure<DistributedLockOption>(configuration.GetSection("DistributedLockOption"));

            services.AddSingleton(sp =>
            {
                return sp.GetRequiredService<IOptions<DistributedLockOption>>().Value;
            });

            if (distributedLockOption.DistributedLockType == DistributedLockTypes.Redis)
                services.ConfigureRedisDistributedLock(distributedLockOption, loggerFactory);
        }


        private static void ConfigureRedisDistributedLock(this IServiceCollection services, DistributedLockOption distributedLockOption, ILoggerFactory loggerFactory = null)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(distributedLockOption.Connection);

            services.AddSingleton(s => redis.GetDatabase());


            RedLockProvider.SetRedLockFactory(distributedLockOption, loggerFactory);
            services.AddSingleton(typeof(IDistributedLockFactory), RedLockProvider.RedLockFactoryObject);

            services.AddSingleton<IDistributedLockManager>(sp =>
            {
                return new RedisDistributedLockManager(RedLockProvider.RedLockFactoryObject, distributedLockOption);
            });
        }
    }
}
