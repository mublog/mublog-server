using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.Common.DTOs.V1.Authentication
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}