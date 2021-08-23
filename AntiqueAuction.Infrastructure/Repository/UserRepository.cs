using System.Threading.Tasks;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace AntiqueAuction.Infrastructure.Repository
{
    // Framework dependent Persons repository pattern implementation
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(AntiqueAuctionDbContext context) : base(context){}

        public Task<User> Get(string username)
        => Context.User.FirstOrDefaultAsync(x => x.Username.ToLower().Contains(username.ToLower()));
    }
}
