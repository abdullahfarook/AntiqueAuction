using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;

namespace AntiqueAuction.Infrastructure.Repository
{
    // Framework dependent AutoBidRepository repository pattern implementation
    public class AutoBidRepository : Repository<AutoBid>, IAutoBidRepository
    {
        public AutoBidRepository(AntiqueAuctionDbContext context) : base(context) { }
    }
}
