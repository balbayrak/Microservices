using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Domain.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
