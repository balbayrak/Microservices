using Microservices.Basket.Application.Repositories;
using Microservices.Basket.Persistence.Repositories.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Basket.Persistence.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPersistenceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingOption = configuration.GetSection("CacheSettings").Get<CacheSetting>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettingOption.Connection;
            });

            services.AddScoped<IBasketRepository, BasketRedisRepository>();
        }
    }
}
