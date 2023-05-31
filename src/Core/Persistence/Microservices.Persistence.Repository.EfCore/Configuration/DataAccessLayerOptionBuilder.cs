using Microservices.Application.Repositories;
using Microservices.Persistence.Repository.EfCore.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Persistence.Repository.EfCore.Configuration
{
    public class DataAccessLayerOptionBuilder
    {
        public readonly IServiceCollection services;

        public DataAccessLayerOptionBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public void ConfigureDataAccessLayer<TDbContext>(Action<DatabaseOption> option)
where TDbContext :DbContext
        {
            DatabaseOption databaseOption = new DatabaseOption();
            option?.Invoke(databaseOption);

            DatabaseType databaseType = DatabaseType.PostgreSQL;

            services.AddDbContext<TDbContext>((options) =>
            {
                databaseType = databaseOption.DatabaseType;

                if (databaseType == DatabaseType.MSSQL)
                {
                    options.UseSqlServer(databaseOption.ConnectionString);
                }
                else if (databaseType == DatabaseType.PostgreSQL)
                {
                    options.UseNpgsql(databaseOption.ConnectionString);
                }
            });

            var transactionBuilder = services.FirstOrDefault(d => d.ServiceType == typeof(ITransactionBuilder));
            if (transactionBuilder == null)
            {
                services.AddScoped(typeof(ITransactionBuilder), sp =>
                {
                    var context = sp.GetRequiredService<TDbContext>();
                    return new EfTransactionBuilder<TDbContext>(context);
                });
            }

        }
    }
}
