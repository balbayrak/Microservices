using Microservices.Application.Repositories;
using Microservices.Order.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Repositories
{
    public interface IOrderRepository : IRepository<Domain.Models.Order, OrderSearchDto>
    {
    }
}
