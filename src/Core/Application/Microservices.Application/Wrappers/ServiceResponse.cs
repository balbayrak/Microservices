using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Wrappers
{
    public class ServiceResponse<T> : BaseReponse
    {
        public T Value { get; set; }

        public ServiceResponse()
        {

        }
        public ServiceResponse(T value)
        {
            Value = value;
        }
    }
}
