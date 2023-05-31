using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Queries.Product
{
    public class GetByIdProductQuery : IRequest<ServiceResponse<ProductDto>>
    {
        public Guid ProductId { get; set; }

        public GetByIdProductQuery(Guid productId)
        {
            ProductId = productId;
        }
        public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, ServiceResponse<ProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            public GetByIdProductHandler(IMapper mapper,
                IProductRepository productRepository,
                ICategoryRepository categoryRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _categoryRepository = categoryRepository;
            }
            public async Task<ServiceResponse<ProductDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);
                var productDto = _mapper.Map<ProductDto>(product);

                if (productDto is not null)
                {
                    var category = await _categoryRepository.GetAsync(product.CategoryId);
                    if (category is not null)
                    {
                        productDto.Category = _mapper.Map<CategoryDto>(category);
                    }
                }

                return new ServiceResponse<ProductDto>
                {
                    Value = productDto,
                    IsSuccess = true
                };
            }
        }
    }
}
