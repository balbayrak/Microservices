using Microservices.Persistence.Repository.EfCore.Migrator;
using Microservices.Stock.Persistence.Repositories.EfCore.Context;

namespace Microservices.Stock.Persistence.Repositories.EfCore.Migrator
{
    public class StockDbMigrator : BaseEfCoreMigrator<StockDbContext>
    {
    }
}
