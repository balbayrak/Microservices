using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.IntegrationEvents
{
    public class OrderCreatedEvent
    {
        public OrderCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public List<OrderItemMessage> OrderItems { get; set; }

        public Guid CorrelationId { get; }
    }
}
