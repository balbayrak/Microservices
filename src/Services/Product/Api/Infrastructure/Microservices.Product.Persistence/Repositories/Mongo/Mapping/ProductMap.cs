using Microservices.Persistence.Repository.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microservices.Product.Persistence.Repositories.Mongo.Mapping
{
    public class ProductMap
    {
        public static void Configure()
        {
            BaseMongoEntityMap<Domain.Models.Product>.ConfigureEntity();

            BsonClassMap.RegisterClassMap<Domain.Models.Product>(map =>
            {
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Price).SetIsRequired(true)
                .SetSerializer(new CharSerializer(BsonType.Decimal128)); ;

                map.MapMember(x => x.CategoryId)
                .SetIsRequired(true)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.UnmapMember(c => c.Category);


            });
        }
    }
}
