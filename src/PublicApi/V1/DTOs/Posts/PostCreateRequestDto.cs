using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Posts
{
    public class PostCreateRequestDto
    {
        [Required]
        public string Content { get; set; }
    }
}