using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Tracing
{
    public interface ICorrelationIdProvider
    {
        ValueTask<Guid?> GetCorrelationIdAsync();
    }
}
