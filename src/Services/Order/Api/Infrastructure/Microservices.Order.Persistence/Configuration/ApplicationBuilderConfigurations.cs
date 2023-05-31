using Microservices.Order.Persistence.Repositories.EfCore.Context;
using Microservices.Order.Persistence.Repositories.EfCore.Migrator;
using Microservices.Persistence.Repository.EfCore.Configuration;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Persistence.Configuration
{
    public static class ApplicationBuilderConfigurations
    {
        public static async Task RunMigratorAsync(this IApplicationBuilder applicationBuilder)
        {
            await applicationBuilder.MigrateDbContextAsync<OrderDbMigrator, OrderDbContext>();
        }
    }
}
