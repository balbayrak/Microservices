using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Tracing;

namespace Microservices.Application.Features.Behaviours
{
    public class CorrelationIdWithHttpContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICorrelationIdHttpContextAccessor _correlationIdHttpContextAccessor;

        public CorrelationIdWithHttpContextBehaviour(ICorrelationIdHttpContextAccessor correlationIdHttpContextAccessor)
        {
            _correlationIdHttpContextAccessor = correlationIdHttpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICorrelated correlated)
            {
                var correlationId = await _correlationIdHttpContextAccessor.GetCorrelationIdAsync();
                if (!string.IsNullOrEmpty(correlationId))
                {
                    correlated.CorrelationId = new Guid(correlationId);
                }
            }
            return await next();
        }
    }
}
