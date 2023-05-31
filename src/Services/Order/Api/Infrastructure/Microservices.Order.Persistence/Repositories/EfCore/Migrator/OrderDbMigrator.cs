using Microservices.Order.Persistence.Repositories.EfCore.Context;
using Microservices.Persistence.Repository.EfCore.Migrator;

namespace Microservices.Order.Persistence.Repositories.EfCore.Migrator
{
    public class OrderDbMigrator : BaseEfCoreMigrator<OrderDbContext>
    {
    }
}
