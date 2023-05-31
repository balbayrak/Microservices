using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Dto
{
    public class OrderCreateDto
    {
        public string BuyerId { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
        public PaymentDto Payment { get; set; }
        public AddressDto Address { get; set; }
    }
}
