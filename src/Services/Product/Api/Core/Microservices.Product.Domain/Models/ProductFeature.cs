using Microservices.Domain;

namespace Microservices.Product.Domain.Models
{
    public class ProductFeature: BaseEntity
    {
        public string Description { get; set; }

        public string SpecialOffers { get; set; }

        public string Promotions { get; set; }
    }
}
