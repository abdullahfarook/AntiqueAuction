using System.Collections.Generic;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using AutoQueryable.AspNetCore.Filter.FilterAttributes;
using Microsoft.AspNetCore.Mvc;

namespace AntiqueAuction.Web.Controllers
{
    [Route("api/bids-history")]
    public class BidHistoryController : ControllerBase
    {
        [HttpGet,AutoQueryable]
        public IEnumerable<BidHistory> Get([FromServices] IBidHistoryRepository repository)
            => repository.Get();
    }
}
