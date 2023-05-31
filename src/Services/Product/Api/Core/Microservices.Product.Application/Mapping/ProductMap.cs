using AutoMapper;
using Microservices.Product.Application.Dto.Product;

namespace Microservices.Product.Application.Mapping
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Domain.Models.Product,ProductDto>().ReverseMap();
            CreateMap<Domain.Models.Product, CreateProductDto>().ReverseMap();
            CreateMap<Domain.Models.Product, UpdateProductDto>().ReverseMap();
            CreateMap<ProductDto, UpdateProductDto>().ReverseMap();
        }
    }
}
