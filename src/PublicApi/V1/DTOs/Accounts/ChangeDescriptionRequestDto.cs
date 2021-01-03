using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class ChangeDescriptionRequestDto
    {
        [Required]
        public string Description { get; set; }
    }
}