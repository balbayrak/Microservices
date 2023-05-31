using MediatR;
using Microservices.Application.Configuration;
using Microservices.Application.Features.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Stock.Application.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddApplicationConfigurations(this IServiceCollection services)
        {
            services.AddApplicationBaseConfigurations(Assembly.GetExecutingAssembly());

           services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CorrelationIdWithIntegrationBehaviour<,>));
        }
    }
}
