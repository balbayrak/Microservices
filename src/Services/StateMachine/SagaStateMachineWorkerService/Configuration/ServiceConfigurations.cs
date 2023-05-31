using Microservices.Application.Common;
using Microservices.Persistence.Repository.EfCore.Configuration;
using Microservices.Persistence.Repository.EfCore.Migrator;
using Microservices.Persistence.Repository.Settings;
using Microsoft.Extensions.Options;
using SagaStateMachineWorkerService.Migrator;
using SagaStateMachineWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.Configuration
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

            services.AddPersistenceEfCoreBaseConfigurations<OrderStateDbContext>(option =>
            {
                option.ConnectionString = databaseSetting.ConnectionString;
                option.DatabaseType = DatabaseType.PostgreSQL;
            });

            services.AddTransient<IEfCoreMigrator, OrderStateDbMigrator>();

            #endregion


        }
    }
}
