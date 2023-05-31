using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Persistence.Repository.Mongo.Extension
{
    public static class MongoRepositoryExtension
    {
        public static FilterDefinition<T> FilterIfAnd<T>(this FilterDefinition<T> builder, bool condition, FilterDefinition<T> filter)
        {
            return condition
                ? builder & filter
                : builder;
        }

        public static FilterDefinition<T> FilterIfOr<T>(this FilterDefinition<T> builder, bool condition, FilterDefinition<T> filter)
        {
            return condition
                ? builder | filter
                : builder;
        }
    }
}
