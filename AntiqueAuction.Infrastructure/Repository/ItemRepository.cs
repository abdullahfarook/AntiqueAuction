using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace AntiqueAuction.Infrastructure.Repository
{
    /// <summary>
    /// Database dependent ItemRepository implementation
    /// Follows Open for extension and close for modification.
    /// </summary>
    public class ItemRepository : Repository<Item>,IItemRepository 
    {
        public ItemRepository(AntiqueAuctionDbContext context) : base(context) { }

        public Task<List<Item>> GetExpired(DateTime time)
        => Context.Items.Where(x=> time >= x.AuctionEnd && x.IsActive).ToListAsync();
    }
}
