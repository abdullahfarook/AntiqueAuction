using System;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Events
{
    public class AuctionExpired:IEvent
    {
        public Guid ItemId { get; set; }

        public AuctionExpired(Guid itemId)
        {
            ItemId = itemId;
        }
    }
}
