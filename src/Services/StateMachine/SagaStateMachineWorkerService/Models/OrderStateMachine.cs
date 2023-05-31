using MassTransit;
using Microservices.Order.IntegrationEvents;
using Microservices.Payment.IntegrationEvents;
using Microservices.Stock.IntegrationEvents;
using Microservices.Stock.IntegrationEvents.Stock;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public Event<OrderCreatedInitializedEvent> OrderCreatedInitializedEvent { get; set; }
        public Event<StockReservedEvent> StockReservedEvent { get; set; }
        public Event<StockNotReservedEvent> StockNotReservedEvent { get; set; }
        public Event<PaymentCompletedEvent> PaymentCompletedEvent { get; set; }
        public Event<PaymentFailedEvent> PaymentFailedEvent { get; set; }

        public State OrderCreated { get; private set; }
        public State StockReserved { get; private set; }
        public State StockNotReserved { get; private set; }
        public State PaymentCompleted { get; private set; }
        public State PaymentFailed { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreatedInitializedEvent, y => y.CorrelateBy<Guid>(order => order.OrderId, ctx => ctx.Message.OrderId).SelectId(context => Guid.NewGuid()));

            Event(() => StockReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => StockNotReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Event(() => PaymentCompletedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

            Initially(
             When(OrderCreatedInitializedEvent)
             .Then(context =>
             {
                 context.Saga.BuyerId = context.Message.BuyerId;
                 context.Saga.OrderId = context.Message.OrderId;
                 context.Saga.CreatedDate = DateTime.UtcNow;
                 context.Saga.CardName = context.Message.OrderPayment.CardName;
                 context.Saga.CardNumber = context.Message.OrderPayment.CardNumber;
                 context.Saga.CVV = context.Message.OrderPayment.CVV;
                 context.Saga.Expiration = context.Message.OrderPayment.Expiration;
                 context.Saga.TotalPrice = context.Message.OrderPayment.TotalPrice;
             })
            .Then(context => { Console.WriteLine($"StockReservedRequestEvent before : {context.Saga}"); })
            .Publish(context => new StockReservedRequestEvent(context.Saga.CorrelationId)
            {
                ReservedItems = context.Message.OrderItems.Select(p => new StockListItem
                {
                    ProductId = p.ProductId,
                    Count = p.Count
                })
            })
            .TransitionTo(OrderCreated)
            .Then(context => { Console.WriteLine($"StockReservedRequestEvent After : {context.Saga}"); }));

            During(OrderCreated,
                When(StockReservedEvent)
                .TransitionTo(StockReserved)
                .Send(new Uri($"queue:{PaymentMessageQueueConst.PaymentRequestStockReservedQueueName}"), context => new PaymentRequestReservedStock(context.Saga.CorrelationId)
                {
                    ReservedItems = context.Message.ReservedItems.Select(p => new PaymentReservedItem
                    {
                        Count = p.Count,
                        ProductId = p.ProductId

                    }).ToList(),
                    Payment = new PaymentMessage()
                    {
                        CardName = context.Saga.CardName,
                        CardNumber = context.Saga.CardNumber,
                        CVV = context.Saga.CVV,
                        Expiration = context.Saga.Expiration,
                        TotalPrice = context.Saga.TotalPrice
                    },
                    BuyerId = context.Saga.BuyerId
                }).Then(context => { Console.WriteLine($"StockReservedEvent After : {context.Saga}"); }),
                When(StockNotReservedEvent).TransitionTo(StockNotReserved).Publish(context => new OrderRequestFailedEvent() { 
                    OrderId = context.Saga.OrderId, 
                    Reason = context.Message.Reason })
                .Then(context => { Console.WriteLine($"StockReservedEvent After : {context.Saga}"); }));

            During(StockReserved,
                When(PaymentCompletedEvent).TransitionTo(PaymentCompleted).Publish(context => new OrderRequestCompletedEvent() { OrderId = context.Saga.OrderId }).Then(context => { Console.WriteLine($"PaymentCompletedEvent After : {context.Saga}"); }).Finalize(),
                When(PaymentFailedEvent)
                .Publish(context => new OrderRequestFailedEvent() { OrderId = context.Saga.OrderId, Reason = context.Message.Reason })
                .Send(new Uri($"queue:{StockMessageQueueConst.StockRollBackMessageQueueName}"), context => new StockRollbackMessage() 
                { ReservedItems = context.Message.ReservedItems.Select(p=> new StockListItem
                {
                    ProductId = p.ProductId,
                    Count = p.Count,
                }).ToList() 
                }).TransitionTo(PaymentFailed).Then(context => { Console.WriteLine($"PaymentFailedEvent After : {context.Saga}"); })
                );

            SetCompletedWhenFinalized();
        }
    }
}
