using System;

namespace AntiqueAuction.Application.Items.Dtos
{
    public class AutomateBid
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public float IncrementFactor { get; set; }
        public double MaxBidAmount { get; set; }
    }
}
