using Microservices.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Microservices.Persistence.Repository.Mongo.Mapping
{
    public class BaseMongoEntityMap<TEntity> where TEntity : class, IEntity
    {
        public static void ConfigureEntity()
        {
            BsonClassMap.RegisterClassMap<TEntity>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(c => c.Id)
                     .SetIdGenerator(new StringObjectIdGenerator())
                     .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.MapMember(t => t.CreationDate)
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Local))
                .SetDefaultValue(DateTime.UtcNow);

                map.MapMember(t => t.LastModificationDate)
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Local))
                .SetDefaultValue(DateTime.UtcNow);

            });
        }
    }
}
