using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Exceptions.Product;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Commands.Product
{
    public class UpdateProductCommand : IRequest<ServiceResponse<ProductDto>>
    {
        public UpdateProductDto updateProductDto { get; set; }

        public UpdateProductCommand(UpdateProductDto dto)
        {
            updateProductDto = dto;
        }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<ProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductRepository productRepository, 
                IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var checkProduct = await _productRepository.GetAsync(request.updateProductDto.Id);
                if (checkProduct != null)
                {
                    var product = _mapper.Map<Domain.Models.Product>(request.updateProductDto);
                    product = await _productRepository.UpdateAsync(product, true);
                    var dto = _mapper.Map<ProductDto>(product);
                    return new ServiceResponse<ProductDto>
                    {
                        IsSuccess = true,
                        Value = dto
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
