using Microservices.Persistence.Repository.Mongo;
using Microservices.Persistence.Repository.Mongo.Context;
using Microservices.Product.Application.Dto.Category;
using Microservices.Product.Application.Repositories;
using Microservices.Product.Domain.Models;

namespace Microservices.Product.Persistence.Repositories.Mongo
{
    public class CategoryMongoRepository : BaseMongoRepository<Category,CategorySearchDto>, ICategoryRepository
    {
        public CategoryMongoRepository(IMongoContext context) : base(context)
        {
        }
    }
}
