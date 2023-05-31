using Microservices.Integration;

namespace Microservices.Payment.IntegrationEvents
{
    public class PaymentFailedEvent : BaseIntegrationEvent
    {
        public string Reason { get; set; }
        public List<PaymentReservedItem> ReservedItems { get; set; }
    }
}
