using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AntiqueAuction.Application.Auth;
using AntiqueAuction.Application.Auth.Dtos;
using AntiqueAuction.Application.Extensions;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Core.Repository;
using AntiqueAuction.Shared.Exceptions;
using AntiqueAuction.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AntiqueAuction.Web.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        [HttpGet("me")]
        [Authorize(policy:"regular")]
        public Task<User> Me([FromServices] IUserRepository userRepo)
            => userRepo.Get(AuthUser!.Id);
        [HttpPost]
        public Task<GenerateTokenResponse> GenerateToken([FromBody] GenerateToken command,
            [FromServices] IUserRepository userRepository)
            => userRepository.Get(command.Username)
                .ContinueWith(x => new GenerateTokenResponse(GenerateJwtToken(x.Result)),
                    TaskContinuationOptions.OnlyOnRanToCompletion);

        [HttpPatch("max-bid")]
        public Task UpdateMaxBid([FromBody] UpdateMaxBid command,
            [FromServices] IAuthService authService)
        {
            command.UserId = AuthUser!.Id;
            return authService.Handle(command);
        }

        private static string GenerateJwtToken(User user)
        {
            if (user is null)
                throw NotFoundException.ForClient("Username or Password is incorrect");
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(DummyAppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Name),    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
