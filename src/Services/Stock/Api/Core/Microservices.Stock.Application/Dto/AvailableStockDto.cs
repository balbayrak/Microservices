using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Application.Dto
{
    public class AvailableStockDto
    {
        public IEnumerable<StockDto> StockDtos { get; set; }
    }
}
