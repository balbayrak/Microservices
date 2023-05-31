using MassTransit;
using Microservices.Integration.Configuration;
using Microservices.Product.Integration.Consumers;
using Microservices.Product.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Product.Integration.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddProductIntegrationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIntegrationConfigurations(configuration, "MassTransit",
                   (IRegistrationConfigurator configurator) =>
                   {
                       configurator.AddConsumers(Assembly.GetExecutingAssembly());
                   },
                (IBusControl busControl, IServiceProvider provider) =>
                {
                    busControl.ConnectReceiveEndpoint(ProductMessageQueueConst.StockSnapshotSyncEventQueueName,
                                                      endpointConfigurator => { endpointConfigurator.Consumer<StockSnapshotSyncConsumer>(provider); });
                });
        }
    }
}
