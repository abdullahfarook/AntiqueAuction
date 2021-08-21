using System;
using System.Collections.Generic;
using System.Linq;
using AntiqueAuction.Shared.Domain;
using AntiqueAuction.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Infrastructure.EventBus
{
    public static class InMemoryEventBusServiceExtension
    {
        private static IEnumerable<Type> _events = new List<Type>();
        public static IServiceCollection RegisterMemoryEventBus(this IServiceCollection services, Type[] types)
        {
            services.AddSingleton<IEventBus, InMemoryEventBus>();
            RegisterEventAndHandlers(services, types);
            return services;
        }
        private static void RegisterEventAndHandlers(IServiceCollection services, Type[] types)
        {
            // configure events
            _events = _events.Concat(types
                .Where(type => typeof(IEvent).IsAssignableFrom(type) && !type.IsInterface));

            // configure event handlers
            types
                .Where(type => typeof(IEventHandler).IsAssignableFrom(type) && !type.IsInterface)
                .ForEach(handler =>
                    handler.GetGenericImplementedInterfaces(typeof(IEventHandler<>))
                        .ForEach(@event => 
                            services.AddTransient(@event, handler)));
        }
    }
}
