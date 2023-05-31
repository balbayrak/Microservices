using Microservices.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.IntegrationEvents
{
    public class OrderRequestCompletedEvent : BaseIntegrationEvent
    {
        public Guid OrderId { get; set; }
    }
}
