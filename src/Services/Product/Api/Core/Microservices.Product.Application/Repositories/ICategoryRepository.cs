using Microservices.Application.Repositories;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Domain.Models;

namespace Microservices.Product.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, CategorySearchDto>
    {
    }
}
