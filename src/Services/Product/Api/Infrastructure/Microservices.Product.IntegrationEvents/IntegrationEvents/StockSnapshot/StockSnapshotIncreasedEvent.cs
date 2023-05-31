using Microservices.Integration;

namespace Microservices.Product.IntegrationEvents.StockSnapshot
{
    public class StockSnapshotIncreasedEvent : BaseIntegrationEvent
    {
        public IEnumerable<ProductListItem> ProductItems { get; set; }
    }
}
