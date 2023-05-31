using AutoMapper;
using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Wrappers;
using Microservices.Integration;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Repositories;
using Microservices.Stock.IntegrationEvents.Stock;
using Microsoft.Extensions.Logging;

namespace Microservices.Product.Application.Features.Commands.Product
{
    public class CreateProductCommand : IRequest<ServiceResponse<Guid>>, ICorrelated
    {
        public CreateProductDto createProductDto { get; set; }
        public Guid CorrelationId { get; set; }

        public CreateProductCommand(CreateProductDto dto)
        {
            createProductDto = dto;
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Guid>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            private readonly IIntegrationEventPublisher _integrationEventPublisher;
            private readonly ILogger<CreateProductCommandHandler> _logger;
            public CreateProductCommandHandler(IProductRepository productRepository,
                IMapper mapper, IIntegrationEventPublisher integrationEventPublisher, ILogger<CreateProductCommandHandler> logger)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _integrationEventPublisher = integrationEventPublisher;
                _logger = logger;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Create product request with correlationId:{request.CorrelationId}");

                var product = _mapper.Map<Domain.Models.Product>(request.createProductDto);
                product = await _productRepository.InsertAsync(product, true);

                _integrationEventPublisher.AddEvent(new StockInitalizedEvent
                {
                    ProductId = product.Id,
                    AvailableStock = request.createProductDto.StockSnapshot != null ? request.createProductDto.StockSnapshot.AvailableStock : 0
                });
                await _integrationEventPublisher.Publish();

                return new ServiceResponse<Guid>
                {
                    IsSuccess = true,
                    Value = product.Id
                };
            }
        }
    }
}
