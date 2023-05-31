using Microservices.Integration;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockSyncEvent : BaseIntegrationEvent
    {
        public IEnumerable<StockListItem> StockList { get; set; }
    }
}
