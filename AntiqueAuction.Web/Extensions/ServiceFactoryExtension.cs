using System;
using System.Linq;
using AntiqueAuction.Application;
using AntiqueAuction.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Web.Extensions
{
    public static class ServiceFactoryExtension
    {
        /// <summary>
        /// Register all Services in IOC using Factory Pattern
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services, Type[] types)
        {
            types
                .Where(type => typeof(ServiceBase).IsAssignableFrom(type) && !type.IsAbstract)
                .ForEach(type => services.AddScoped(type.GetInterface($"I{type.Name}")!, type));
            return services;
        }
    }
}
