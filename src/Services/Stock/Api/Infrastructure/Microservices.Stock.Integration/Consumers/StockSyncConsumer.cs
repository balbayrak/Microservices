using MassTransit;
using MediatR;
using Microservices.Integration;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Features.Commands;
using Microservices.Stock.IntegrationEvents.Stock;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Stock.Integration.Consumers
{
    public class StockSyncConsumer : IConsumer<StockSyncEvent>
    {
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public StockSyncConsumer(IMediator mediator,
            IIntegrationEventPublisher integrationPublisher, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task Consume(ConsumeContext<StockSyncEvent> context)
        {
            StockSyncEvent @event = context.Message;

            UpdateStockCommand updateStockCommand = new UpdateStockCommand();
            updateStockCommand.StockDtos = @event.StockList.Select(p => new StockDto
            {
                Count = p.Count,
                ProductId = p.ProductId,
                //StockMotionId = 
            });

            var updateResponse = await _mediator.Send(updateStockCommand);

            if (updateResponse.IsSuccess)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IIntegrationEventPublisher _integrationEventPublisher = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();
                    _integrationEventPublisher.AddEvent(new StockReservedEvent
                    {
                        ReservedItems = @event.StockList
                    });

                    await _integrationEventPublisher.Publish();
                }
            }
        }
    }
}
