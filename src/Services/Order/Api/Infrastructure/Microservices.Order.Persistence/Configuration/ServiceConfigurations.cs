using Microservices.Application.Common;
using Microservices.Order.Application.Repositories;
using Microservices.Order.Integration.Configuration;
using Microservices.Order.Persistence.Repositories;
using Microservices.Order.Persistence.Repositories.EfCore.Context;
using Microservices.Order.Persistence.Repositories.EfCore.Migrator;
using Microservices.Persistence.Repository.EfCore.Configuration;
using Microservices.Persistence.Repository.EfCore.Migrator;
using Microservices.Persistence.Repository.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microservices.Order.Persistence.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPersistenceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database 

            var databaseSetting = new DatabaseSettings();
            configuration.GetSection("DatabaseSettings").Bind(databaseSetting);

            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDataBaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddPersistenceEfCoreBaseConfigurations<OrderDbContext>(option =>
            {
                option.ConnectionString = databaseSetting.ConnectionString;
                option.DatabaseType = DatabaseType.PostgreSQL;
            });

            services.AddScoped<IOrderRepository, OrderEfCoreRepository>();

            services.AddTransient<IEfCoreMigrator, OrderDbMigrator>();

            #endregion


            #region IntegrationEventPublisher

            services.AddOrderIntegrationConfigurations(configuration);

            #endregion

        }
    }
}
