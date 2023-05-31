using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Application.Dto
{
    public class StockDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public Guid? StockMotionId { get; set; }
    }
}
