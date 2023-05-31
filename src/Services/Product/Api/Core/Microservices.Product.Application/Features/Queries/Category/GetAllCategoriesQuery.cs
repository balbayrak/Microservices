using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Queries.Category
{
    public class GetAllCategoriesQuery : IRequest<PagedResponse<List<CategoryDto>>>
    {
        public CategorySearchDto CategorySearchDto { get; set; }

        public GetAllCategoriesQuery(CategorySearchDto categorySearchDto)
        {
            CategorySearchDto = categorySearchDto;
        }
        public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, PagedResponse<List<CategoryDto>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }
            public async Task<PagedResponse<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categoryListResponse = await _categoryRepository.GetAllAsync(request.CategorySearchDto, cancellationToken);
                var dtos = _mapper.Map<List<CategoryDto>>(categoryListResponse.Value);
                return new PagedResponse<List<CategoryDto>>
                {
                    Value = dtos,
                    IsSuccess = true,
                    TotalCount = categoryListResponse.TotalCount
                };
            }
        }
    }
}
