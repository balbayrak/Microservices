using Microservices.Application.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Integration.Correlation
{
    public class NullCorrelationIdProvider : ICorrelationIdProvider
    {
        public async ValueTask<Guid?> GetCorrelationIdAsync()
        {
            return null;
        }
    }
}
