using MassTransit;
using MediatR;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Features.Commands.Stock;
using Microservices.Stock.IntegrationEvents.Stock;

namespace Microservices.Stock.Integration.Consumers
{
    public class StockInitConsumer : IConsumer<StockInitalizedEvent>
    {
        private readonly IMediator _mediator;

        public StockInitConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<StockInitalizedEvent> context)
        {
            StockInitalizedEvent @event = context.Message;

            CreateStockDto createStockDto = new CreateStockDto();
            createStockDto.ProductId = @event.ProductId;
            createStockDto.AvailableStock = @event.AvailableStock;
            createStockDto.StockMotionId = null;

            await _mediator.Send(new CreateStockCommand(createStockDto));
        }
    }
}
