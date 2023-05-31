using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Persistence.Repository.EfCore.Migrator
{
    public interface IEfCoreMigrator
    {
        Task MigrateDbContextAsync(IServiceProvider serviceProvider);
    }
}
