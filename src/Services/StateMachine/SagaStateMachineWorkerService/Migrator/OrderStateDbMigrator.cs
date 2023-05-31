using Microservices.Persistence.Repository.EfCore.Migrator;
using SagaStateMachineWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.Migrator
{
    public class OrderStateDbMigrator : BaseEfCoreMigrator<OrderStateDbContext>
    {
    }
}
