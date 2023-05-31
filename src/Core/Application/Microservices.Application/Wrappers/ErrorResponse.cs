using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Wrappers
{
    public class ErrorResponse
    {
        public ErrorResponse(string friendlyMessage, string exceptionType = null)
        {
            FriendlyMessage = friendlyMessage ?? throw new ArgumentNullException(nameof(friendlyMessage));
            ExceptionType = exceptionType;
        }

        public string FriendlyMessage { get; }
        public string ExceptionType { get; }
    }
}
