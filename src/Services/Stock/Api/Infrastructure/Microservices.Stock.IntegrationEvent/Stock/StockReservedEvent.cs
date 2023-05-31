using Microservices.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockReservedEvent : BaseIntegrationEvent
    {
        public StockReservedEvent()
        {

        }
        public StockReservedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public IEnumerable<StockListItem> ReservedItems { get; set; }
    }
}
