using Microservices.Persistence.Repository.EfCore.Context;
using Microservices.Persistence.Repository.EfCore.Migrator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Persistence.Repository.EfCore.Configuration
{
    public static class ApplicationBuilderConfigurations
    {
        public static async Task MigrateDbContextAsync<TMigrator,TDbContext>(this IApplicationBuilder applicationBuilder)
            where TDbContext : CoreDbContext
            where TMigrator : BaseEfCoreMigrator<TDbContext>, IEfCoreMigrator
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var migrator = serviceProvider.GetRequiredService<IEfCoreMigrator>() as TMigrator;

                await migrator.MigrateDbContextAsync(serviceProvider);
            }
        }
    }
}
