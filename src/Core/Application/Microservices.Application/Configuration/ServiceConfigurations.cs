using FluentValidation;
using MediatR;
using Microservices.Application.Features.Behaviours;
using Microservices.Application.Tracing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Application.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddApplicationBaseConfigurations(this IServiceCollection services, Assembly assembly)
        {
           
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        }
    }
}
