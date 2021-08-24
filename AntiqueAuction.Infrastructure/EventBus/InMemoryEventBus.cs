using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AntiqueAuction.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AntiqueAuction.Infrastructure.EventBus
{
    public class InMemoryEventBus:IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        {

            foreach (var @event in events)
            {
                var type = typeof(IEventHandler<>);
                var make = type.MakeGenericType(@event.GetType());
                var handlers = _serviceProvider.GetServices(make);

                foreach (var handler in handlers)
                {
                    var genericType = handler!.GetType();
                    var method = genericType.GetMethod("Handle");
                    var result = (Task) method!.Invoke(handler, new[] {@event,(object)cancellationToken});
                    await result;
                }

            }
        }

        public Task Publish(IEvent @event, CancellationToken cancellationToken = default)
            => Publish(new[] {@event}, cancellationToken);

    }
}
