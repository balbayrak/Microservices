using MassTransit;
using MediatR;
using Microservices.Integration.Configuration;
using Microservices.Integration.Correlation;
using Microservices.Stock.Integration.Consumers;
using Microservices.Stock.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Stock.Integration.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddStockIntegrationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIntegrationConfigurations(configuration, "MassTransit",
                (IRegistrationConfigurator configurator) =>
                {
                    configurator.AddConsumers(Assembly.GetExecutingAssembly());
                },
                (IBusControl busControl, IServiceProvider provider) =>
                {
                    busControl.ConnectReceiveEndpoint(StockMessageQueueConst.StockInitEventQueueName,
                                                      endpointConfigurator => { endpointConfigurator.Consumer<StockInitConsumer>(provider); });

                    busControl.ConnectReceiveEndpoint(StockMessageQueueConst.StockSyncEventQueueName,
                                                      endpointConfigurator => { endpointConfigurator.Consumer<StockSyncConsumer>(provider); });

                    busControl.ConnectReceiveEndpoint(StockMessageQueueConst.StockReservedRequestEventQueueName,
                                  endpointConfigurator => { endpointConfigurator.Consumer<StockReservedRequestConsumer>(provider); });


                    busControl.ConnectReceiveEndpoint(StockMessageQueueConst.StockRollBackMessageQueueName,
                                  endpointConfigurator => { endpointConfigurator.Consumer<StockRollBackMessageConsumer>(provider); });

                });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IntegrationCorrelationIdBehaviour<,>));
        }
    }
}



