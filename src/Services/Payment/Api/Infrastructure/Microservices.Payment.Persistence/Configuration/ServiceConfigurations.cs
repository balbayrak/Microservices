using Microservices.Payment.Integration.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Payment.Persistence.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddPersistenceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region IntegrationEventPublisher

            services.AddPaymentIntegrationConfigurations(configuration);

            #endregion

        }
    }
}
