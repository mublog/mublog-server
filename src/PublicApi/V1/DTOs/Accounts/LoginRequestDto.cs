using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}