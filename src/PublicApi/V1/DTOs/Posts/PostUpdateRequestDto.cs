using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Posts
{
    public class PostUpdateRequestDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}