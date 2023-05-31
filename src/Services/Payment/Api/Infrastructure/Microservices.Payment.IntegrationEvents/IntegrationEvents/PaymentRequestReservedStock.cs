using Microservices.Integration;

namespace Microservices.Payment.IntegrationEvents
{
    public class PaymentRequestReservedStock : BaseIntegrationEvent
    {
        public PaymentRequestReservedStock(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public PaymentMessage Payment { get; set; }
        public List<PaymentReservedItem> ReservedItems { get; set; }
        public string BuyerId { get; set; }
    }
}
