using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class ChangePasswordRequestDto
    {
        [Required]
        public string CurrentPassword1 { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}