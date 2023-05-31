using Microservices.Persistence.Repository.Mongo.Mapping;
using Microservices.Product.Persistence.Repositories.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Microservices.Product.Persistence.Repositories.Mongo
{
    public class ProductMongoDbMapping 
    {
        public static void ConfigureMapping()
        {
            MongoDbMapping.Configure();

            BsonClassMap.RegisterClassMap<ProductMap>();
            BsonClassMap.RegisterClassMap<CategoryMap>();
        }
    }
}
