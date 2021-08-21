using System.Threading.Tasks;
using AntiqueAuction.Application.Items;
using AntiqueAuction.Application.Items.Dtos;
using Coravel.Invocable;

namespace AntiqueAuction.Web.Scheduler
{
    /// <summary>
    /// Scheduled event handler to remove all expired items
    /// </summary>
    public class AuctionScheduler:IInvocable
    {
        private readonly IItemService _itemService;

        public AuctionScheduler(IItemService itemService)
        {
            _itemService = itemService;
        }

        public Task Invoke()
        => _itemService.Handle(new ExpireOldAuctions());
    }
}
