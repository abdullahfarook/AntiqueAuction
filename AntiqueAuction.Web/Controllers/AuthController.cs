using AntiqueAuction.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AntiqueAuction.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


    }
}
