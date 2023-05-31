using Microservices.Persistence.Repository.EfCore.Configuration;
using Microservices.Stock.Persistence.Repositories.EfCore.Context;
using Microservices.Stock.Persistence.Repositories.EfCore.Migrator;
using Microsoft.AspNetCore.Builder;

namespace Microservices.Stock.Persistence.Configuration
{
    public static class ApplicationBuilderConfigurations
    {
        public static async Task RunMigratorAsync(this IApplicationBuilder applicationBuilder)
        {
            await applicationBuilder.MigrateDbContextAsync<StockDbMigrator, StockDbContext>();
        }
    }
}
