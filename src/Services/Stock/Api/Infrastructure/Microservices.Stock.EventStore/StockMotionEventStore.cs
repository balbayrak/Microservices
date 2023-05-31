using EventStore.ClientAPI;
using Microservices.EventSourcing.EventStores;
using Microservices.Stock.Application.EventStore;

namespace Microservices.Stock.EventStore
{
    public class StockMotionEventStore : BaseEventStream, IStockMotionEventStore
    {
        public StockMotionEventStore(IEventStoreConnection eventStoreConnection) : base(EventStoreConstants.STOCK_MOTION_EVENT_STREAM_NAME, eventStoreConnection)
        {
        }

        public async Task<bool> CreateStockMotionEvent(StockMotionEvent stockMotionEvent)
        {
            Events.AddLast(stockMotionEvent);
            await SaveAsync();
            return true;
        }
    }
}
