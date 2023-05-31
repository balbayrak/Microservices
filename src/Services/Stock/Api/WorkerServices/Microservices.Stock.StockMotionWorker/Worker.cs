using EventStore.ClientAPI;
using Microservices.EventSourcing.BackgroundService;
using Microservices.Integration;
using Microservices.Product.IntegrationEvents.StockSnapshot;
using Microservices.Stock.Application.EventStore;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.StockMotionWorker
{
    public class Worker : EventSourcingBackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(IEventStoreConnection eventStoreConnection, IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger) : base(eventStoreConnection)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public override string StreamName => EventStoreConstants.STOCK_MOTION_EVENT_STREAM_NAME;

        public override string GroupName => EventStoreConstants.STOCK_MOTION_EVENT_STREAM_GROUP_NAME;

        public override async Task ExecuteAppearedEvent(object eventData)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IIntegrationEventPublisher _integrationEventPublisher = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();
                if (eventData is StockMotionEvent { StockMotionType: StockMotionTypeEnum.InitializeStock } stockMotionInitializeEvent)
                {
                    // _integrationEventPublisher.AddEvent()
                }
                else if (eventData is StockMotionEvent { StockMotionType: StockMotionTypeEnum.AddStock } stockMotionAddEvent)
                {
                    _integrationEventPublisher.AddEvent(new StockSnapshotIncreasedEvent
                    {
                        CorrelationId = stockMotionAddEvent.CorrelationId,
                        ProductItems = stockMotionAddEvent.StockDatas.Select(p => new ProductListItem
                        {
                            ProductId = p.ProductId,
                            Count = p.Count
                        })
                    }) ;
                }
                else if (eventData is StockMotionEvent { StockMotionType: StockMotionTypeEnum.RemoveStock } stockMotionRemoveEvent)
                {
                    _integrationEventPublisher.AddEvent(new StockSnapshotDecreasedEvent
                    {
                        CorrelationId = stockMotionRemoveEvent.CorrelationId,
                        ProductItems = stockMotionRemoveEvent.StockDatas.Select(p => new ProductListItem
                        {
                            ProductId = p.ProductId,
                            Count = p.Count
                        })
                    });
                }
                else if (eventData is StockMotionEvent { StockMotionType: StockMotionTypeEnum.ResetStock } stockMotionResetEvent)
                {
                    _integrationEventPublisher.AddEvent(new StockSnapshotDecreasedEvent
                    {
                        CorrelationId = stockMotionResetEvent.CorrelationId,
                        ProductItems = stockMotionResetEvent.StockDatas.Select(p => new ProductListItem
                        {
                            ProductId = p.ProductId,
                            Count = p.Count
                        })
                    });
                }

                await _integrationEventPublisher.Publish();
            }
        }

        public override Type GetEventType(string typeName)
        {
            return Type.GetType($"{typeName},Microservices.Stock.EventStore");
        }

    }
}