using MassTransit;
using MediatR;
using Microservices.Integration.Configuration;
using Microservices.Integration.Correlation;
using Microservices.Payment.Integration.Consumer;
using Microservices.Payment.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Payment.Integration.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPaymentIntegrationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIntegrationConfigurations(configuration, "MassTransit",
                (IRegistrationConfigurator configurator) =>
                {
                    configurator.AddConsumers(Assembly.GetExecutingAssembly());
                },
                (IBusControl busControl, IServiceProvider provider) =>
                {
                    busControl.ConnectReceiveEndpoint(PaymentMessageQueueConst.PaymentRequestStockReservedQueueName,
                                                      endpointConfigurator => { endpointConfigurator.Consumer<PaymentRequestConsumer>(provider); });

                });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IntegrationCorrelationIdBehaviour<,>));
        }
    }
}
