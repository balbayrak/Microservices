using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Wrappers
{
    public class PagedResponse<T> : ServiceResponse<T>
    {
        public PagedResponse() : base()
        {

        }
        public PagedResponse(T value) : base(value)
        {
        }

        public int TotalCount { get; set; }
    }
}
