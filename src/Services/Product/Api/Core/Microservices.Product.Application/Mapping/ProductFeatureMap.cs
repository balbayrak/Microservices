using AutoMapper;
using Microservices.Product.Application.Dto.ProductFeature;
using Microservices.Product.Domain.Models;

namespace Microservices.Product.Application.Mapping
{
    public class ProductFeatureMap : Profile
    {
        public ProductFeatureMap()
        {
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
        }
    }
}
