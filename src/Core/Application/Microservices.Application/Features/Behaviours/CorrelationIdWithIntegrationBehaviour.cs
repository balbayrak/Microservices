using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Tracing;

namespace Microservices.Application.Features.Behaviours
{
    public class CorrelationIdWithIntegrationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public CorrelationIdWithIntegrationBehaviour(ICorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICorrelated correlated)
            {
                var correlationId = await _correlationIdProvider.GetCorrelationIdAsync();
                if (correlationId.HasValue)
                {
                    correlated.CorrelationId = correlationId.Value;
                }
            }
            return await next();
        }
    }
}
