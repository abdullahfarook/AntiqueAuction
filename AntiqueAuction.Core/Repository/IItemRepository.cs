using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntiqueAuction.Core.Models;

namespace AntiqueAuction.Core.Repository
{
    // Framework independent Items repository interface
    public interface IItemRepository:IRepository<Item>
    {
        Task<List<Item>> GetExpired(DateTime time);
    }
}
