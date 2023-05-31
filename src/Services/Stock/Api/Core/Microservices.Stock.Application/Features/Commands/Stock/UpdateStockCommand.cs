using AutoMapper;
using MediatR;
using Microservices.Application.Common;
using Microservices.Application.Repositories;
using Microservices.Application.Wrappers;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.Application.Exceptions;
using Microservices.Stock.Application.Repositories;

namespace Microservices.Stock.Application.Features.Commands
{
    public class UpdateStockCommand : IRequest<ServiceResponse<bool>>, ICorrelated
    {
        public Guid CorrelationId { get; set; }

        public IEnumerable<StockDto> StockDtos { get; set; }

        public class UpdateAvailableStockCommandHandler : IRequestHandler<UpdateStockCommand, ServiceResponse<bool>>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IMapper _mapper;
            private readonly ITransactionBuilder _transactionBuilder;

            public UpdateAvailableStockCommandHandler(IStockRepository stockRepository,
                IMapper mapper,
                ITransactionBuilder transactionBuilder)
            {
                _stockRepository = stockRepository;
                _mapper = mapper;
                _transactionBuilder = transactionBuilder;
            }

            public async Task<ServiceResponse<bool>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
            {
                if (request.StockDtos != null)
                {
                    await _transactionBuilder.BeginTransactionAsync();
                    
                    try
                    {
                        foreach (var stockDto in request.StockDtos)
                        {
                            var stock = await _stockRepository.GetByProductId(new StockSearchDto
                            {
                                ProductId = stockDto.ProductId
                            });
                            if (stock != null)
                            {
                                stock.AvailableStock = stockDto.Count;
                                //stock.StockMotionId = stockDto.StockMotionId;
                                await _stockRepository.UpdateAsync(stock, false);
                            }
                            else
                            {
                                throw new StockNotFoundException();
                            }
                        }

                        await _transactionBuilder.CommitTransactionAsync();
                        return new ServiceResponse<bool>
                        {
                            IsSuccess = true,
                            Value = true
                        };

                    }
                    catch(Exception ex)
                    {
                        await _transactionBuilder.RollbackTransactionAsync();
                        throw ex;
                    }
                    finally
                    {
                        _transactionBuilder.DisposeTransaction();
                    }

                }

                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Value = false
                };

            }
        }
    }
}
