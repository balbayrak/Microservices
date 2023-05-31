using AutoMapper;
using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Wrappers;
using Microservices.Integration;
using Microservices.Order.Application.Dto;
using Microservices.Order.Application.Repositories;
using Microservices.Order.IntegrationEvents;
using System.Reflection.Metadata.Ecma335;

namespace Microservices.Order.Application.Features.Commands.Order
{
    public class CreateOrderCommand : IRequest<ServiceResponse<Guid>>, ICorrelated
    {
        public OrderCreateDto orderCreateDto { get; set; }
        public Guid CorrelationId { get; set; }

        public CreateOrderCommand(OrderCreateDto dto)
        {
            orderCreateDto = dto;
        }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Guid>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            private readonly IIntegrationEventPublisher _integrationEventPublisher;

            public CreateOrderCommandHandler(IOrderRepository orderRepository,
                IMapper mapper, IIntegrationEventPublisher integrationEventPublisher)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
                _integrationEventPublisher = integrationEventPublisher;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = _mapper.Map<Domain.Models.Order>(request.orderCreateDto);
                order = await _orderRepository.InsertAsync(order, true);

                if (order != null && order.Id != Guid.Empty)
                {
                    var orderCreatedRequestEvent = new OrderCreatedInitializedEvent()
                    {
                        CorrelationId = order.Id,
                        BuyerId = request.orderCreateDto.BuyerId,
                        OrderId = order.Id,
                        OrderPayment = new OrderPaymentMessage
                        {
                            CardName = request.orderCreateDto.Payment.CardName,
                            CardNumber = request.orderCreateDto.Payment.CardNumber,
                            Expiration = request.orderCreateDto.Payment.Expiration,
                            CVV = request.orderCreateDto.Payment.CVV,
                            TotalPrice = request.orderCreateDto.Items.Sum(x => x.Price * x.Count)
                        },
                    };

                    request.orderCreateDto.Items.ToList().ForEach(item =>
                    {
                        orderCreatedRequestEvent.OrderItems.Add(new OrderItemMessage { Count = item.Count, ProductId = item.ProductId });
                    });

                    _integrationEventPublisher.AddEvent(orderCreatedRequestEvent);
                    await _integrationEventPublisher.Publish();

                    return new ServiceResponse<Guid>(order.Id);
                }

                return new ServiceResponse<Guid>
                {
                    IsSuccess = false,
                    Value = Guid.Empty
                };
            }
        }
    }
}
