using Microservices.Application.Common;
using Microservices.EventSourcing.Configuration;
using Microservices.Persistence.Repository.EfCore.Configuration;
using Microservices.Persistence.Repository.EfCore.Migrator;
using Microservices.Persistence.Repository.Settings;
using Microservices.Stock.Application.Repositories;
using Microservices.Stock.EventStore;
using Microservices.Stock.Integration.Configuration;
using Microservices.Stock.Persistence.Repositories.EfCore;
using Microservices.Stock.Persistence.Repositories.EfCore.Context;
using Microservices.Stock.Persistence.Repositories.EfCore.Migrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microservices.Stock.Persistence.Configuration
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

            services.AddPersistenceEfCoreBaseConfigurations<StockDbContext>(option=>
            {
                option.ConnectionString = databaseSetting.ConnectionString;
                option.DatabaseType = DatabaseType.PostgreSQL;
            });

            services.AddScoped<IStockRepository, StockEfCoreRepository>();

            services.AddTransient<IEfCoreMigrator, StockDbMigrator>();

            #endregion

            #region EventStore

            services.AddEventStore(configuration, "EventStore");

            services.AddSingleton<IStockMotionEventStore, StockMotionEventStore>();

            #endregion

            #region IntegrationEventPublisher

            services.AddStockIntegrationConfigurations(configuration);

            #endregion

        }
    }
}
