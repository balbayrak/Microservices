using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace Microservices.EventSourcing.BackgroundService
{
    public abstract class EventSourcingBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        protected readonly IEventStoreConnection _eventStoreConnection;

        public abstract string StreamName { get; }

        public abstract string GroupName { get; }

        public EventSourcingBackgroundService(IEventStoreConnection eventStoreConnection)
        {
            _eventStoreConnection = eventStoreConnection;
        }

        public virtual new Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
                await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(StreamName, GroupName, EventAppeared, autoAck: false);

        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2)
        {
            var typeName = $"{Encoding.UTF8.GetString(arg2.Event.Metadata)}";
            var type = GetEventType(typeName);
            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            await ExecuteAppearedEvent(@event);

            arg1.Acknowledge(arg2.Event.EventId);
        }

        public virtual new Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        public abstract Task ExecuteAppearedEvent(object eventData);

        public abstract Type GetEventType(string typeName);
    }
}
