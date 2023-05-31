using Microservices.Integration;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockInitalizedEvent : BaseIntegrationEvent
    {
        public Guid ProductId { get; set; }
        public int AvailableStock { get; set; }
    }
}
