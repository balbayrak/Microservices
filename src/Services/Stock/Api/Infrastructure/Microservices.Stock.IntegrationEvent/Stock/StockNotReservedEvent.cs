using Microservices.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockNotReservedEvent : BaseIntegrationEvent
    {
        public string Reason { get; set; }
    }
}
