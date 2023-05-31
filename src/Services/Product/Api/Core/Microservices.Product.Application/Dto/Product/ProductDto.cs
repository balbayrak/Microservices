using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Dto.ProductFeature;
using Microservices.Product.Application.Dto.StockSnapshot;

namespace Microservices.Product.Application.Dto.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public CategoryDto Category { get; set; }

        public ProductFeatureDto ProductFeature { get; set; }

        public StockSnapshotDto StockSnapshot { get; set; }


    }
}
