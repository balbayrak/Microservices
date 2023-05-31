using MassTransit;
using MediatR;
using Microservices.Integration;
using Microservices.Stock.Application.Features.Commands.StockMotion;
using Microservices.Stock.Application.Repositories;
using Microservices.Stock.IntegrationEvents.Stock;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Stock.Integration.Consumers
{
    public class StockReservedRequestConsumer : IConsumer<StockReservedRequestEvent>
    {
        private readonly IMediator _mediator;
        private readonly IStockRepository _stockRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StockReservedRequestConsumer(IMediator mediator,
            IStockRepository stockRepository,
            IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _stockRepository = stockRepository;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<StockReservedRequestEvent> context)
        {
            bool allProductReserved = true;
            foreach (var reservedItem in context.Message.ReservedItems)
            {
                var stock = await _stockRepository.GetByProductId(new Application.Dto.StockSearchDto
                {
                    ProductId = reservedItem.ProductId
                });

                if (stock.AvailableStock >= reservedItem.Count) continue;
                else
                {
                    allProductReserved = false;
                    break;
                }
            }
            if (allProductReserved)
            {
                RemoveStockMotionCommand removeStockMotionCommand = new RemoveStockMotionCommand();
                removeStockMotionCommand.StockDtos = context.Message.ReservedItems.Select(p => new Application.Dto.StockDto
                {
                    Count = p.Count,
                    ProductId = p.ProductId
                });

                await _mediator.Send(removeStockMotionCommand);
            }
            else
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IIntegrationEventPublisher _integrationEventPublisher = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();
                    _integrationEventPublisher.AddEvent(new StockNotReservedEvent
                    {
                        CorrelationId = context.Message.CorrelationId,
                        Reason = "Stock yetersiz"
                    });

                    await _integrationEventPublisher.Publish();
                }
            }
        }
    }
}
