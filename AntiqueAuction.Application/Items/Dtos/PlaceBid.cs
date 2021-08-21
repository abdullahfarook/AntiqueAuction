using System;
using System.ComponentModel.DataAnnotations;
using AntiqueAuction.Shared.Extensions;

namespace AntiqueAuction.Application.Items.Dtos
{
    public class PlaceBid
    {
        [Required]
        [NotDefault]
        public Guid ItemId { get; set; }

        [Required]
        [NotDefault]
        public Guid UserId { get; set; }

        [Required]
        [NotDefault]
        public double Amount { get; set; }
    }
    
}
