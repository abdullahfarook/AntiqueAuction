using System.Collections.Generic;
using System.Threading.Tasks;
using AntiqueAuction.Application.Items;
using AntiqueAuction.Application.Items.Dtos;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using AutoQueryable.AspNetCore.Filter.FilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace AntiqueAuction.Web.Controllers
{
    
    [Route("api/items")]
    public class ItemsController:ControllerBase
    {
        [HttpGet, AutoQueryable]
        public IEnumerable<Item> Get([FromServices] IItemRepository repository)
            => repository.Get();
        [HttpPost]
        public Task PlaceBid([FromBody] PlaceBid command,[FromServices] IItemService itemService)
            => itemService.Handle(command);
        [HttpPut]
        public Task AutomateBid([FromBody] AutomateBid command, [FromServices] IItemService itemService)
            => itemService.Handle(command);
    }
}
