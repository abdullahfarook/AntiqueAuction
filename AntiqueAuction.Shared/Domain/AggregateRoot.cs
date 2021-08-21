using System.Collections.Generic;

namespace AntiqueAuction.Shared.Domain
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IEvent> _domainEvents = new List<IEvent>();
        public IReadOnlyList<IEvent> DomainEvents() => _domainEvents;

        protected void RaiseDomainEvent(IEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
