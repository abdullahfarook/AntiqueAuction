using System;
using System.Linq;
using AntiqueAuction.Infrastructure.Repository;
using AntiqueAuction.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Web.Extensions
{
    public static class RepositoryFactoryExtension
    {
        /// <summary>
        /// Register repositories using factory pattern
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Type[] types)
        {
            types
                .Where(type => type.BaseType is {IsGenericType: true} &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(Repository<>) &&
                               !type.IsInterface)
                .ForEach(type => services.AddScoped(type.GetInterface($"I{type.Name}")!, type));
            return services;
        }
    }
}
