using Microservices.Application.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Basket.Application.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddApplicationConfigurations(this IServiceCollection services)
        {
            services.AddApplicationBaseConfigurations(Assembly.GetExecutingAssembly());
        }
    }
}
