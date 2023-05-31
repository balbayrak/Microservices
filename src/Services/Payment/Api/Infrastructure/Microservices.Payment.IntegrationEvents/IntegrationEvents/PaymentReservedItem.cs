using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Payment.IntegrationEvents
{
    public class PaymentReservedItem
    {
        public Guid ProductId { get; set; }

        public int Count { get; set; }
    }
}
