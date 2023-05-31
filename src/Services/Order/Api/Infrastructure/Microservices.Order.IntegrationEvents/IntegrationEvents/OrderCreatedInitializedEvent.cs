using Microservices.Integration;

namespace Microservices.Order.IntegrationEvents
{
    public class OrderCreatedInitializedEvent : BaseIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
        public OrderPaymentMessage OrderPayment { get; set; }
    }
}
