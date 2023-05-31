using AutoMapper;
using Microservices.Application.Wrappers;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.EventStore;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Features.Commands.StockMotion
{
    public class RemoveStockMotionCommand : BaseStockMotionCommand<ServiceResponse<bool>>
    {
        public override StockMotionTypeEnum StockMotionType => StockMotionTypeEnum.RemoveStock;

        public class RemoveStockCommandHandler : BaseStockMotionCommandHandler<RemoveStockMotionCommand, ServiceResponse<bool>>
        {
            public RemoveStockCommandHandler(IStockMotionEventStore stockMotionEventStore,IMapper mapper) : base(stockMotionEventStore,mapper)
            {
            }
        }
    }
}
