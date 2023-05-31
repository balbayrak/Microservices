using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.IntegrationEvents.Stock
{
    public class StockListItem
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
