using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Application.Tracing
{
    public class CorrelationIdHttpContextAccessor : ICorrelationIdHttpContextAccessor
    {
        public const string CorrelationIdHeaderKey = "X-Correlation-ID";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<string> GetCorrelationIdAsync()
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(
                         CorrelationIdHeaderKey, out StringValues values))
            {
                return Task.FromResult(values.First());
            }

            return Task.FromResult(string.Empty);
        }
    }
}
