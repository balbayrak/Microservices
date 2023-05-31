using Microservices.Application.Repositories;
using Microservices.Persistence.Repository.Mongo.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Persistence.Repository.Mongo.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPersistenceMongoBaseConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
        }
    }
}
