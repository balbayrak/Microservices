using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Dto
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
