using MassTransit;
using MediatR;
using Microservices.Integration.Configuration;
using Microservices.Integration.Correlation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Integration.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddOrderIntegrationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIntegrationConfigurations(configuration, "MassTransit",
                (IRegistrationConfigurator configurator) =>
                {
                    configurator.AddConsumers(Assembly.GetExecutingAssembly());
                },
                  (IBusControl busControl, IServiceProvider provider) =>
                  {

                  });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IntegrationCorrelationIdBehaviour<,>));
        }
    }
}
