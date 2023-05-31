using MassTransit;
using MediatR;
using Microservices.Stock.Application.Features.Commands.StockMotion;
using Microservices.Stock.IntegrationEvents.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Stock.Integration.Consumers
{
    public class StockRollBackMessageConsumer : IConsumer<StockRollbackMessage>
    {
        private readonly IMediator _mediator;

        public StockRollBackMessageConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<StockRollbackMessage> context)
        {
            AddStockMotionCommand addStockMotionCommand = new AddStockMotionCommand();
            addStockMotionCommand.StockDtos = context.Message.ReservedItems.Select(p => new Application.Dto.StockDto
            {
                Count = p.Count,
                ProductId = p.ProductId
            });

            await _mediator.Send(addStockMotionCommand);
        }
    }
}
