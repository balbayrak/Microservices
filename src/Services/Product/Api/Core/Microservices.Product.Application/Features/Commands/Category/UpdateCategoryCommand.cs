using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Exceptions.Category;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Commands.Category
{
    public class UpdateCategoryCommand : IRequest<ServiceResponse<CategoryDto>>
    {
        public UpdateCategoryDto UpdateCategoryDto { get; set; }

        public UpdateCategoryCommand(UpdateCategoryDto dto)
        {
            UpdateCategoryDto = dto;
        }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ServiceResponse<CategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository,
                IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var checkCategory = await _categoryRepository.GetAsync(request.UpdateCategoryDto.Id, cancellationToken);
                if (checkCategory != null)
                {
                    var category = _mapper.Map<Domain.Models.Category>(request.UpdateCategoryDto);
                    category = await _categoryRepository.UpdateAsync(category, true);
                    var dto = _mapper.Map<CategoryDto>(category);

                    return new ServiceResponse<CategoryDto>(dto);
                }
                else
                {
                    throw new CategoryNotFoundException();
                }
            }
        }
    }
}
