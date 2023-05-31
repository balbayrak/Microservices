using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.IntegrationEvents.StockSnapshot
{
    public class ProductListItem
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
