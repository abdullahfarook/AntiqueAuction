using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;

namespace AntiqueAuction.Infrastructure.Repository
{
    // Framework dependent Persons repository pattern implementation
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(AntiqueAuctionDbContext context) : base(context){}
    }
}
