using Microservices.Application.Common;
using Microservices.Persistence.Repository.Mongo.Configuration;
using Microservices.Persistence.Repository.Settings;
using Microservices.Product.Application.Repositories;
using Microservices.Product.Integration.Configuration;
using Microservices.Product.Persistence.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Microservices.Product.Persistence.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPersistenceConfigurations(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddPersistenceMongoBaseConfigurations();

            #region Database

            ProductMongoDbMapping.ConfigureMapping();

            services.AddScoped<IProductRepository, ProductMongoRepository>();
            services.AddScoped<ICategoryRepository, CategoryMongoRepository>();

            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDataBaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            #endregion

            #region IntegrationEventPublisher

            services.AddProductIntegrationConfigurations(configuration);

            #endregion
        }
    }
}
