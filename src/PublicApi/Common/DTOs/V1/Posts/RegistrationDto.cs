using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.Common.DTOs.V1.Posts
{
    public class RegistrationDto
    {
        [Required] public string Email { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}