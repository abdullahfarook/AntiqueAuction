using System.Collections.Generic;
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
    [Route("api/items")]
    public class ItemsController:BaseController
    {
        [HttpGet, AutoQueryable]
        public IEnumerable<Item> Get([FromServices] IItemRepository repository)
            => repository.Get();

        [HttpPost]
        public Task PlaceBid([FromBody] PlaceBid command, [FromServices] IItemService itemService)
        {
            command.UserId = AuthUser!.Id;
            return itemService.Handle(command);
        }
       
    }
}
