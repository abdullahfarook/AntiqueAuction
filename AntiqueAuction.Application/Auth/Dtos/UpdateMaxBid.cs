using System;
using AntiqueAuction.Shared.Extensions;

namespace AntiqueAuction.Application.Auth.Dtos
{
    public class UpdateMaxBid
    {
        [NotDefault]
        public double Amount { get; set; }
        [NotDefault]
        public Guid UserId { get; set; }
    }
}
