using System;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Events
{
    public class BidPlaced:IEvent
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; }
        public double LastBid { get; set; }

        public BidPlaced(Guid itemId, Guid userId, double lastBid)
        {
            ItemId = itemId;
            UserId = userId;
            LastBid = lastBid;
        }
    }
}
