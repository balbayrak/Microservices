using Microservices.Persistence.Repository.EfCore.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace Microservices.Persistence.Repository.EfCore.Migrator
{
    public abstract class BaseEfCoreMigrator<TDbContext> : IEfCoreMigrator
        where TDbContext : DbContext
    {
        public async Task MigrateDbContextAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TDbContext>>();
            var context = serviceProvider.GetRequiredService<TDbContext>();
            try
            {
                logger.LogInformation("Migration Database associated with context {DbContext}", typeof(TDbContext).Name);
                var retry = Policy.Handle<SqlException>().WaitAndRetry(new List<TimeSpan>()
                {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(30)
                });
                await retry.Execute(async ()=>
                {
                    await context.Database.EnsureCreatedAsync();
                    await context.Database.MigrateAsync();
                });
               
                logger.LogInformation("Database migrated");

                await SeedAsync(context);
            }
            catch (Exception e)
            {
                logger.LogError("an error occured while migration the database used on context {DbContext}", typeof(TDbContext).Name);
            }
        }

        public virtual Task SeedAsync(TDbContext dbContext)
        {
            return Task.CompletedTask;
        }
    }
}
