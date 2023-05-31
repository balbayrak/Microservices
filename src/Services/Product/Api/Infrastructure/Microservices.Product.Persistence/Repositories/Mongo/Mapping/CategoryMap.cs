
using Microservices.Persistence.Repository.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Microservices.Product.Persistence.Repositories.Mongo.Mapping
{
    public class CategoryMap
    {
        public static void Configure()
        {
            BaseMongoEntityMap<Domain.Models.Category>.ConfigureEntity();

            BsonClassMap.RegisterClassMap<Domain.Models.Category>(map =>
            {
                map.MapMember(x => x.Name).SetIsRequired(true);
            });
        }
    }
}
