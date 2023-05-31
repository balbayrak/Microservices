using Microservices.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.EventStore
{
    public class StockMotionEvent : IEvent
    {
        public IEnumerable<StockData> StockDatas { get; set; }
        public StockMotionTypeEnum StockMotionType { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
