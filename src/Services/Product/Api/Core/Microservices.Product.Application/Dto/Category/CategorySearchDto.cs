using Microservices.Application.Dto;

namespace Microservices.Product.Application.Dto.Category
{
    public class CategorySearchDto : PagedSearchDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }
    }
}
