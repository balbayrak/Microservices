using AutoMapper;
using MediatR;
using Microservices.Application.Wrappers;
using Microservices.Product.Application.Exceptions.Category;
using Microservices.Product.Application.Repositories;

namespace Microservices.Product.Application.Features.Commands.Category
{
    public class DeleteCategoryCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid CategoryId { get; set; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ServiceResponse<bool>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository,
                IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(request.CategoryId);
                if (category != null)
                {
                    await _categoryRepository.DeleteAsync(category, true);
                    return new ServiceResponse<bool>(true);
                }
                else
                {
                    throw new CategoryNotFoundException();
                }
            }
        }
    }
}
