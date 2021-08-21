using System;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Models
{
    public class BidHistory:Entity
    {
        public Guid ItemId { get; protected set; }
        public Guid UserId { get; protected set; }
        public double Amount { get; protected set; }
        public virtual Item Item { get; protected set; }
        public virtual User User { get; protected set; }

        protected BidHistory(){}

        public BidHistory(Item item, User user, double amount)
        {
            ItemId = item.Id;
            Item = item;
            UserId = user.Id;
            Item = item;
            Amount = amount;
        }
    }
}
