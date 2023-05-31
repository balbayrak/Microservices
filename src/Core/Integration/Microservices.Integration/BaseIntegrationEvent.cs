using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Integration
{
    public abstract class BaseIntegrationEvent : IIntegrationEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
