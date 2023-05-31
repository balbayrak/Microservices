using Microservices.Integration;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockReservedRequestEvent : BaseIntegrationEvent
    {
        public StockReservedRequestEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public IEnumerable<StockListItem> ReservedItems { get; set; }
    }
}
