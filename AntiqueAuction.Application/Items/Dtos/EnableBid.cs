using System;

namespace AntiqueAuction.Application.Items.Dtos
{
    public class EnableBid
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
