using System;
using System.Threading;
using System.Threading.Tasks;
using AntiqueAuction.Core.Events;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Application.BidHistory
{
    
    public class BidHistoryEventHandler : IEventHandler<BidPlaced>
    {
        public Task Handle(BidPlaced @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
