using System;
using System.Threading.Tasks;
using AntiqueAuction.Application.Items.Dtos;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using AntiqueAuction.Shared.Exceptions;

namespace AntiqueAuction.Application.Items
{
    public interface IItemService
    {
        Task Handle(EnableBid command);
        Task Handle(PlaceBid command);
        Task Handle(ExpireOldAuctions command);
    }
    public class ItemService:ServiceBase,IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

        public ItemService(IItemRepository itemRepository,IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(EnableBid command)
        {
            Validate(command);
            var (user, item) = await Get(command.UserId, command.ItemId);
            item.EnableAutoBid(user, 1);
            await _itemRepository.Update(item);
        }

        public async Task Handle(PlaceBid command)
        {
            Validate(command);
            var (user, item) =await Get(command.UserId, command.ItemId);
            item.PlaceBid(user,command.Amount);
            await _itemRepository.Update(item);
        }

        public async Task Handle(ExpireOldAuctions command)
        {
            Validate(command);
            var auctionItems = await _itemRepository.GetExpired(DateTime.UtcNow);
            auctionItems.ForEach(x => x.ExpireNow());
            await _itemRepository.Update(auctionItems);
        }

        private async Task<(User user, Item item)> Get(Guid userId, Guid itemId)
        {
            var user = await _userRepository.Find(userId);
            if (user is null)
                throw NotFoundException.ForSystem("No UserId provided from Session");

            var auctionItem = await _itemRepository.Find(itemId);
            if (auctionItem is null)
                throw NotFoundException.ForClient($"No Item found against ID: {itemId}");
            return (user, auctionItem);
        }
    }
}
