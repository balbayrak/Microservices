using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Exceptions.Product;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Commands.Product
{
    public class DeleteProductCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid ProductId { get; set; }

        public DeleteProductCommand(Guid productId)
        {
            ProductId = productId;
        }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<bool>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public DeleteProductCommandHandler(IProductRepository productRepository, 
                IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var checkProduct = await _productRepository.GetAsync(request.ProductId);
                if (checkProduct != null)
                {
                    var product = _mapper.Map<Domain.Models.Product>(request.ProductId);
                    product = await _productRepository.InsertAsync(product, true);
                    return new ServiceResponse<bool>
                    {
                        IsSuccess = true,
                        Value = true
                    };
                }
                else
                {
                    throw new ProductNotFoundException();
                }
            }
        }
    }
}
