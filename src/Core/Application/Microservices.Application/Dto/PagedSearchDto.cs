using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Dto
{
    public abstract class PagedSearchDto
    {
        public int PageSize { get; set; } = 10;

        public int Page { get; set; } = 0;
    }
}
