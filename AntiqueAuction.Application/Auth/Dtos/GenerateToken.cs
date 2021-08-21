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
}
