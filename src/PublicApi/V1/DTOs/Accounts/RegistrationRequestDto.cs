using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}