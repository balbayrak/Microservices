namespace Microservices.Stock.Application.Dto
{
    public class CreateStockDto
    {
        public Guid ProductId { get; set; }
        public int AvailableStock { get; set; }
        public Guid? StockMotionId { get; set; }
    }
}
