using System;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Models
{
    public class AutoBid:Entity
    {
        public Guid ItemId { get; protected set; }
        public Guid UserId { get; protected set; }
        public float IncrementPerUnit { get; protected set; }
        public double MaxBidAmount { get; protected set; }
        public bool IsActive { get; set; }

        public virtual Item Item { get; protected set; }
        public virtual User User { get; protected set; }

        protected AutoBid(){}

        public AutoBid(Item item, User user, float incrementPerUnit, double maxBidAmount)
        {
            ItemId = item.Id;
            UserId = user.Id;
            IncrementPerUnit = incrementPerUnit;
            MaxBidAmount = maxBidAmount;
        }

        public void Update(float incrementPerUnit, double maxBidAmount)
        {
            IncrementPerUnit = incrementPerUnit;
            MaxBidAmount = maxBidAmount;

        }
        public void DisableAutoBidding()
        {
            IsActive = false;
        }
    }
}
