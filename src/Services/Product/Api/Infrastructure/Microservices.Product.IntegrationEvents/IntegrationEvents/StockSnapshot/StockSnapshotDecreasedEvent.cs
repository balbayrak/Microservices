using Microservices.Integration;

namespace Microservices.Product.IntegrationEvents.StockSnapshot
{
    public class StockSnapshotDecreasedEvent : BaseIntegrationEvent
    {
        public IEnumerable<ProductListItem> ProductItems { get; set; }
    }
}
