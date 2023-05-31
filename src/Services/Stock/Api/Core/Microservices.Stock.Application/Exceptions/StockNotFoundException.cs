using Microservices.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Application.Exceptions
{
    public class StockNotFoundException : NotFoundException
    {
        public StockNotFoundException() : base(ExceptionMessages.STOCK_NOT_FOUND)
        {
        }
    }
}
