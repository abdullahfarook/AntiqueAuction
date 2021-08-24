using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiqueAuction.Application.Items;
using AntiqueAuction.Application.Items.Dtos;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using AutoQueryable.AspNetCore.Filter.FilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntiqueAuction.Web.Controllers
{
    [Authorize("regular")]
    [Route("api/auto-bids")]
    public class AutoBidController : BaseController
    {
        [HttpGet("/api/items/{itemId:guid}/auto-bids"), AutoQueryable]
        public IEnumerable<AutoBid> Get([FromRoute] Guid itemId, [FromServices] IAutoBidRepository repository)
            => repository.Get().Where(x=> x.ItemId == itemId).AsQueryable();
        [HttpPut]
        [Authorize]
        public Task AutomateBid([FromBody] EnableBid command, [FromServices] IItemService itemService)
        {
            command.UserId = AuthUser!.Id;
            return itemService.Handle(command);
        }

    }
}
