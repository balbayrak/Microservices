using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Features.Commands.StockMotion
{
    public class BaseStockMotionCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, ServiceResponse<bool>>
        where TRequest : BaseStockMotionCommand<ServiceResponse<bool>>
    {
        protected readonly IStockMotionEventStore StockMotionEventStore;
        private readonly IMapper _mapper;

        public BaseStockMotionCommandHandler(IStockMotionEventStore stockMotionEventStore, IMapper mapper)
        {
            StockMotionEventStore = stockMotionEventStore;
            _mapper = mapper;
        }

        public virtual async Task<ServiceResponse<bool>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var @event = new StockMotionEvent
            {
                StockDatas = _mapper.Map<IEnumerable<StockData>>(request.StockDtos),
                CorrelationId = request.CorrelationId,
                StockMotionType = request.StockMotionType
            };

            var result = await StockMotionEventStore.CreateStockMotionEvent(@event);

            return new ServiceResponse<bool>(result);
        }
    }
}
