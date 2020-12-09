using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class ChangeDisplayNameRequestDto
    {
        [Required]
        public string DisplayName { get; set; }
    }
}