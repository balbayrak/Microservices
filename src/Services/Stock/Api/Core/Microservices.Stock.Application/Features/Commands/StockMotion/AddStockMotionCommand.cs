using AutoMapper;
using Microservices.Application.Wrappers;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Features.Commands.StockMotion
{
    public class AddStockMotionCommand : BaseStockMotionCommand<ServiceResponse<bool>>
    {
        public override StockMotionTypeEnum StockMotionType => StockMotionTypeEnum.AddStock;

        public class AddStockCommandHandler : BaseStockMotionCommandHandler<AddStockMotionCommand, ServiceResponse<bool>>
        {
            public AddStockCommandHandler(IStockMotionEventStore stockMotionEventStore,IMapper mapper) : base(stockMotionEventStore,mapper)
            {
            }
        }
    }
}
