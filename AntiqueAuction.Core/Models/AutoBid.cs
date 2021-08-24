using System;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Models
{
    public class AutoBid:Entity
    {
        public Guid ItemId { get; protected set; }
        public Guid UserId { get; protected set; }
        public bool IsActive { get; set; }

        public virtual Item Item { get; protected set; }
        public virtual User User { get; protected set; }

        protected AutoBid(){}

        public AutoBid(Item item, User user,bool isActive =true)
        {
            ItemId = item.Id;
            UserId = user.Id;
            IsActive = isActive;
        }
        public void DisableAutoBidding()
        {
            IsActive = false;
        }
        public void EnableAutoBidding()
        {
            IsActive = true;
        }
    }
}
