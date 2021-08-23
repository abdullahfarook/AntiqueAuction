#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AntiqueAuction.Web.Controllers
{
    public class BaseController:ControllerBase
    {
        public AuthenticateUser? AuthUser
        {
            get
            {
                var id = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid);
                if (id is null) return null;
                return new AuthenticateUser(
                    new Guid(id.Value),
                    User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value,
                    User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value);
            }
        }
        

    }

    public class AuthenticateUser
    {
        public AuthenticateUser(Guid id, string? username, string? name)
        {
            Id = id;
            Username = username;
            Name = name;
        }

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
    }
}
