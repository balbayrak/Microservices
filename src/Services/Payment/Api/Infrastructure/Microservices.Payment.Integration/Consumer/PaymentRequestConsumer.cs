using MassTransit;
using Microservices.Integration;
using Microservices.Payment.IntegrationEvents;

namespace Microservices.Payment.Integration.Consumer
{
    public class PaymentRequestConsumer : IConsumer<PaymentRequestReservedStock>
    {
        private IIntegrationEventPublisher _integrationEventPublisher;

        public PaymentRequestConsumer(IIntegrationEventPublisher integrationEventPublisher)
        {
            _integrationEventPublisher = integrationEventPublisher;
        }

        public async Task Consume(ConsumeContext<PaymentRequestReservedStock> context)
        {
            var balance = 3000m;

            if (balance > context.Message.Payment.TotalPrice)
            {
                _integrationEventPublisher.AddEvent(new PaymentCompletedEvent());
                //_logger.LogInformation($"{context.Message.payment.TotalPrice} TL was withdrawn from credit card for user id= {context.Message.BuyerId}");
            }
            else
            {
                // _logger.LogInformation($"{context.Message.payment.TotalPrice} TL was not withdrawn from credit card for user id={context.Message.BuyerId}");
                _integrationEventPublisher.AddEvent(new PaymentFailedEvent
                {
                    Reason = "not enough balance",
                    ReservedItems = context.Message.ReservedItems
                });
            }

            await _integrationEventPublisher.Publish();
        }
    }
}
