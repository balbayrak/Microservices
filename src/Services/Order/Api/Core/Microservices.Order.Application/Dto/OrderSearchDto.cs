using Microservices.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Dto
{
    public class OrderSearchDto : PagedSearchDto
    {
        public string BuyerId { get; set; }
    }
}
