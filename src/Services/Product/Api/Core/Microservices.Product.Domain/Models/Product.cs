using Microservices.Domain;

namespace Microservices.Product.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }

        public StockSnapshot StockSnapshot { get; set; }
    }
}
