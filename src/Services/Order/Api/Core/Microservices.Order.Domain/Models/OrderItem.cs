using Microservices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Domain.Models
{
    public class OrderItem :BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public virtual Order Order { get; set; }

    }
}
