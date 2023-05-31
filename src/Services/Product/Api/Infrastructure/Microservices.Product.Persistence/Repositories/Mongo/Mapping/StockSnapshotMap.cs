
using Microservices.Persistence.Repository.Mongo.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microservices.Product.Persistence.Repositories.Mongo.Mapping
{
    public class StockSnapshotMap
    {
        public static void Configure()
        {
            BaseMongoEntityMap<Domain.Models.StockSnapshot>.ConfigureEntity();

            BsonClassMap.RegisterClassMap<Domain.Models.StockSnapshot>(map =>
            {
                map.MapMember(x => x.AvailableStock).SetIsRequired(true);

            });


        }
    }
}
