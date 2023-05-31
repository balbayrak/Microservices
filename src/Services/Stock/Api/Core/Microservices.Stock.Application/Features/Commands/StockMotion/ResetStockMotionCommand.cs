using AutoMapper;
using Microservices.Application.Wrappers;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Features.Commands.StockMotion
{
    public class ResetStockMotionCommand : BaseStockMotionCommand<ServiceResponse<bool>>
    {
        public override StockMotionTypeEnum StockMotionType => StockMotionTypeEnum.ResetStock;

        public class ResetStockCommandHandler : BaseStockMotionCommandHandler<ResetStockMotionCommand, ServiceResponse<bool>>
        {
            public ResetStockCommandHandler(IStockMotionEventStore stockMotionEventStore,IMapper mapper) : base(stockMotionEventStore,mapper)
            {
            }
        }
    }
}
