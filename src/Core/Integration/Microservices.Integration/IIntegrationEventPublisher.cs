using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Integration
{
    public interface IIntegrationEventPublisher
    {
        IReadOnlyList<IIntegrationEvent> IntegrationEvents { get; }
        void AddEvent(IIntegrationEvent integrationEvent);
        Task Publish(CancellationToken cancellationToken = default);
    }
}
