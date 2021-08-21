using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;

namespace AntiqueAuction.Infrastructure.Repository
{
    // Framework dependent AutoBidRepository repository pattern implementation
    public class BidHistoryRepository : Repository<BidHistory>, IBidHistoryRepository
    {
        public BidHistoryRepository(AntiqueAuctionDbContext context) : base(context) { }
    }
}
