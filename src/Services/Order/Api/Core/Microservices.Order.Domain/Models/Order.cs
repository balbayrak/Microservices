using Microservices.Domain;
using Microservices.Order.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Domain.Models
{
    public class Order : BaseEntity
    {
        public string BuyerId { get; set; }
        public Address Address { get; set; }
        public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public string FailMessage { get; set; }
    }
}
