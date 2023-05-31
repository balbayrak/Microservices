using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(Exception ex) : this(ex.Message)
        {
        }
    }
}
