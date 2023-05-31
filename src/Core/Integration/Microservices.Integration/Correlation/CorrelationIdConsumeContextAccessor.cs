using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Integration.Correlation
{
    public class CorrelationIdConsumeContextAccessor : ICorrelationIdConsumeContextAccessor
    {
        private readonly ConsumeContext _consumeContext;
        public CorrelationIdConsumeContextAccessor(ConsumeContext consumeContext)
        {
            _consumeContext = consumeContext;
        }

        public Task<string> GetCorrelationIdAsync()
        {
            if (_consumeContext != null && _consumeContext.CorrelationId.HasValue)
            {
                return Task.FromResult(_consumeContext.CorrelationId.Value.ToString());
            }

            return Task.FromResult(string.Empty);
        }
    }
}
