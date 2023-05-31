using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Dto
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
