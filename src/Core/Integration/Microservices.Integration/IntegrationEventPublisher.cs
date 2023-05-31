using MassTransit;
using Microservices.Application.Common;
using Microservices.Application.Tracing;
using System.Collections.Concurrent;

namespace Microservices.Integration
{
    public class IntegrationEventPublisher : IIntegrationEventPublisher,
                                            IDisposable
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICorrelationIdProvider _correlationIdProvider;
        private readonly ConcurrentQueue<IIntegrationEvent> _events;

        public IntegrationEventPublisher(ICorrelationIdProvider correlationIdProvider,
            ISendEndpointProvider sendEndpointProvider,
            IPublishEndpoint publishEndpoint)
        {
            _events = new ConcurrentQueue<IIntegrationEvent>();
            _correlationIdProvider = correlationIdProvider;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public IReadOnlyList<IIntegrationEvent> IntegrationEvents => _events.ToList().AsReadOnly();

        public void AddEvent(IIntegrationEvent integrationEvent)
        {
            _events.Enqueue(integrationEvent);
        }

        public async Task Publish(CancellationToken cancellationToken = default)
        {
            var publishTasks = new List<Task>();

            while (_events.TryDequeue(out IIntegrationEvent integrationEvent))
            {
                if (integrationEvent is ICorrelated && integrationEvent.CorrelationId == Guid.Empty)
                {
                    var correlationId = await _correlationIdProvider.GetCorrelationIdAsync();
                    if (correlationId.HasValue)
                    {
                        integrationEvent.CorrelationId = correlationId.Value;
                    }
                }
                if (integrationEvent is IIntegrationSendEvent)
                {
                    var sendIntegrationEvent = integrationEvent as IIntegrationSendEvent;
                    var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{sendIntegrationEvent.QueueName}"));

                    await sendEndpoint.Send(sendIntegrationEvent.EventData);

                    Task publishTask = _sendEndpointProvider.Send(integrationEvent, integrationEvent.GetType(), cancellationToken);
                    publishTasks.Add(publishTask);
                }
                else
                {
                    Task publishTask = _publishEndpoint.Publish(integrationEvent, integrationEvent.GetType(), cancellationToken);
                    publishTasks.Add(publishTask);
                }
            }

            await Task.WhenAll(publishTasks);
        }

        public void Dispose()
        {
            _events?.Clear();
        }
    }
}
