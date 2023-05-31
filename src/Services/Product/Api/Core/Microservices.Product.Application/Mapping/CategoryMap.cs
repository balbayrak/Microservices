using AutoMapper;
using Microservices.Product.Application.Dto.Category;

namespace Microservices.Product.Application.Mapping
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Domain.Models.Category, CategoryDto>().ReverseMap();
            CreateMap<Domain.Models.Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
