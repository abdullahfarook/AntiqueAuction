using System.ComponentModel.DataAnnotations;

namespace AntiqueAuction.Application.Auth.Dtos
{
    public class GenerateToken
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class GenerateTokenResponse
    {
        public GenerateTokenResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
    }
}
