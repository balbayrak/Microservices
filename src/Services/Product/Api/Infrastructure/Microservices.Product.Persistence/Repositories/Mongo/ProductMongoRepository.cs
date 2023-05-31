using Microservices.Persistence.Repository.Mongo;
using Microservices.Persistence.Repository.Mongo.Context;
using Microservices.Persistence.Repository.Mongo.Extension;
using Microservices.Product.Application.Dto.Product;
using Microservices.Product.Application.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace Microservices.Product.Persistence.Repositories.Mongo
{
    public class ProductMongoRepository : BaseMongoRepository<Domain.Models.Product,ProductSearchDto>,IProductRepository
    {
        public ProductMongoRepository(IMongoContext context) : base(context)
        {
        }

        protected override FilterDefinition<Domain.Models.Product> CreateFilteredQuery(ProductSearchDto searchEntity)
        {
            var filter = base.CreateFilteredQuery(searchEntity);
            filter = filter.FilterIfAnd(searchEntity.Id.HasValue && searchEntity.Id != Guid.Empty, Builders<Domain.Models.Product>.Filter.Eq(p => p.Id, searchEntity.Id));
            filter = filter.FilterIfAnd(searchEntity.Name is not null, Builders<Domain.Models.Product>.Filter.Regex(p => p.Name, new BsonRegularExpression($"/{searchEntity.Name}/")));
            filter = filter.FilterIfAnd(searchEntity.Code is not null, Builders<Domain.Models.Product>.Filter.Regex(p => p.Code, new BsonRegularExpression($"/{searchEntity.Code}/")));
            filter = filter.FilterIfAnd(searchEntity.CategoryId.HasValue && searchEntity.CategoryId != Guid.Empty, Builders<Domain.Models.Product>.Filter.Eq(p => p.CategoryId,searchEntity.CategoryId));
            filter = filter.FilterIfAnd(searchEntity.Description is not null, Builders<Domain.Models.Product>.Filter.Regex(p => p.ProductFeature.Description, new BsonRegularExpression(new Regex($"^{searchEntity.Description}.*", RegexOptions.IgnoreCase))));

            return filter;
        }
    }
}
