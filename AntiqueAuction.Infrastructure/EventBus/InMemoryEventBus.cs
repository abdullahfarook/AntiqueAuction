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
        //private readonly MediatrPublisher _mediatr;
        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //_mediatr = new MediatrPublisher(serviceFactory, ParallelWhenAll);
        }
        //public Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        //=> _mediatr.Publish(events, cancellationToken);

        //public Task Publish(IEvent @event, CancellationToken cancellationToken = default)
        //=> _mediatr.Publish(@event, cancellationToken);
        //private static Task ParallelWhenAll(IEnumerable<Func<INotification, CancellationToken, Task>> handlers, INotification notification, CancellationToken cancellationToken)
        //{
        //    var handles = 
        //    var tasks = handlers
        //        .Select(handler =>
        //            Task.Run(() => handler(notification, cancellationToken), cancellationToken))
        //        .ToList();

        //    return Task.WhenAll(tasks);
        //}
        public async Task Publish(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
        {

            foreach (var @event in events)
            {
                var type = typeof(IEventHandler<>);
                var make = type.MakeGenericType(@event.GetType());
                var handlers = _serviceProvider.GetServices(make);
                foreach (dynamic handler in handlers)
                {
                    await handler!.Handle(@event, cancellationToken).ConfigureAwait(false);
                }

            }
        }

        public Task Publish(IEvent @event, CancellationToken cancellationToken = default)
            => Publish(new[] {@event}, cancellationToken);

    }
}
