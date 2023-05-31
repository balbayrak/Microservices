using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Queries.Category
{
    public class GetByIdCategoryQuery : IRequest<ServiceResponse<CategoryDto>>
    {
        public Guid CategoryId { get; set; }

        public GetByIdCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
        public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, ServiceResponse<CategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            public GetByIdCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }
            public async Task<ServiceResponse<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(request.CategoryId, cancellationToken);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return new ServiceResponse<CategoryDto>
                {
                    Value = categoryDto,
                    IsSuccess = true,
                };
            }
        }
    }
}
