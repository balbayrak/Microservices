using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Dto
{
    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
    }
}
