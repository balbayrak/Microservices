using Microservices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Domain.Models
{
    public class StockSnapshot : BaseEntity
    {
        public int AvailableStock { get; private set; }
    }
}
