using MediatR;
using Microservices.Application.Common;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Features.Commands.StockMotion
{
    public abstract class BaseStockMotionCommand<TResponse> : IRequest<TResponse>, ICorrelated
    {
        public Guid CorrelationId { get; set; }
        public IEnumerable<StockDto> StockDtos { get; set; }
        public abstract StockMotionTypeEnum StockMotionType { get; }
    }
}
