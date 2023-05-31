using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Queries.Product
{
    public class GetAllProductsQuery : IRequest<PagedResponse<List<ProductDto>>>
    {
        public ProductSearchDto ProductSearchDto { get; set; }

        public GetAllProductsQuery(ProductSearchDto productSearchDto)
        {
            ProductSearchDto = productSearchDto;
        }
        public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<List<ProductDto>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            public GetAllProductsHandler(IMapper mapper,
                IProductRepository productRepository,
                ICategoryRepository categoryRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
                _categoryRepository = categoryRepository;
            }
            public async Task<PagedResponse<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var productListResponse = await _productRepository.GetAllAsync(request.ProductSearchDto, cancellationToken);
                var productDtos = _mapper.Map<List<ProductDto>>(productListResponse.Value);

                if (productDtos != null)
                {
                    for (int i = 0; i < productDtos.Count; i++)
                    {
                        var category = await _categoryRepository.GetAsync(productListResponse.Value[i].CategoryId);
                        if (category is not null)
                        {
                            productDtos[i].Category = _mapper.Map<CategoryDto>(category);
                        }
                    }
                }

                return new PagedResponse<List<ProductDto>>
                {
                    Value = productDtos,
                    IsSuccess = true,
                    TotalCount = productListResponse.TotalCount
                };
            }
        }
    }
}
