using Microservices.Product.Application.Dto.ProductFeature;
using Microservices.Product.Application.Dto.StockSnapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Application.Dto.Product
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public ProductFeatureDto ProductFeature { get; set; }
        public StockSnapshotDto StockSnapshot { get; set; }
    }
}
