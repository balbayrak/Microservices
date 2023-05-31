using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Commands.Category
{
    public class CreateCategoryCommand : IRequest<ServiceResponse<Guid>>
    {
        public CreateCategoryDto createCategoryDto { get; set; }

        public CreateCategoryCommand(CreateCategoryDto dto)
        {
            createCategoryDto = dto;
        }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ServiceResponse<Guid>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
                IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Domain.Models.Category>(request.createCategoryDto);
                category = await _categoryRepository.InsertAsync(category,true);
                return new ServiceResponse<Guid>(category.Id);
            }
        }
    }
}
