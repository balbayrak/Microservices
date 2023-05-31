using Microservices.Application.Repositories;
using Microservices.Product.Application.Dto.Product;

namespace Microservices.Product.Application.Repositories
{
    public interface IProductRepository : IRepository<Domain.Models.Product,ProductSearchDto>
    {
    }
}
