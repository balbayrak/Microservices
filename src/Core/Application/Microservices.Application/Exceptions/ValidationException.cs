using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Exceptions
{
    public abstract class ValidationException : BaseException
    {
        protected ValidationException(string message) : base(message)
        {
        }
    }
}
