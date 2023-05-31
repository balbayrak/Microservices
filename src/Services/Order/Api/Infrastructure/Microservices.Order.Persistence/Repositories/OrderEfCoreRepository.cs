using Microservices.Order.Application.Dto;
using Microservices.Order.Application.Repositories;
using Microservices.Order.Persistence.Repositories.EfCore.Context;
using Microservices.Persistence.Repository.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Persistence.Repositories
{
    public class OrderEfCoreRepository : BaseEfCoreRepository<OrderDbContext, Domain.Models.Order, OrderSearchDto>, IOrderRepository
    {
        public OrderEfCoreRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
