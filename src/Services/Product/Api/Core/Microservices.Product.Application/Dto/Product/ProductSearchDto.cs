using Microservices.Application.Dto;

namespace Microservices.Product.Application.Dto.Product
{
    public class ProductSearchDto : PagedSearchDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Guid? CategoryId { get; set; }

        public string Description { get; set; }
    }
}
