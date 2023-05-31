using AutoMapper;
using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Wrappers;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Repositories;

namespace Microservices.Stock.Application.Features.Commands.Stock
{
    public class CreateStockCommand : IRequest<ServiceResponse<Guid>>, ICorrelated
    {
        public CreateStockDto createStockDto { get; set; }
        public Guid CorrelationId { get; set; }

        public CreateStockCommand(CreateStockDto dto)
        {
            createStockDto = dto;
        }

        public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, ServiceResponse<Guid>>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IMapper _mapper;

            public CreateStockCommandHandler(IStockRepository stockRepository,
                IMapper mapper)
            {
                _stockRepository = stockRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
            {
                var stock = _mapper.Map<Domain.Models.Stock>(request.createStockDto);
                stock = await _stockRepository.InsertAsync(stock, true);
                return new ServiceResponse<Guid>(stock.Id);
            }
        }
    }
}
