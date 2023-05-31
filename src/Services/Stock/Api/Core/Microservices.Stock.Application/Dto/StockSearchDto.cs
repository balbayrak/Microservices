using Microservices.Application.Dto;

namespace Microservices.Stock.Application.Dto
{
    public class StockSearchDto : PagedSearchDto
    {
        public Guid ProductId { get; set; }
        public Guid StockMotionId { get; set; }
    }
}
