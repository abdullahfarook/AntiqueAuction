using System.Threading;
using System.Threading.Tasks;

namespace AntiqueAuction.Shared.Domain
{
    public interface IEventHandler
    {
    }
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
