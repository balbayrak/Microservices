using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.IntegrationEvents
{
    public class StockMessageQueueConst
    {
        public const string StockInitEventQueueName = "stock-init-queue";

        public const string StockSyncEventQueueName = "stock-sync-queue";

        public const string StockReservedRequestEventQueueName = "stock-reserved-request-queue";

        public const string StockNotReservedEventQueueName = "stock-not-reserved-queue";

        public const string StockRollBackMessageQueueName = "stock-rollback-queue";
    }
}
