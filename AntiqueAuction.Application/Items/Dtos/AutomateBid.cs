using System;

namespace AntiqueAuction.Application.Items.Dtos
{
    public class AutomateBid
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
