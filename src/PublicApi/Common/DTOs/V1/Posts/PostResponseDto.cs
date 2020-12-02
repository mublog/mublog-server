using System;

namespace Mublog.Server.PublicApi.Common.DTOs.V1.Posts
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateEdited { get; set; }
        public int LikeAmount { get; set; }
        public PostUserResponseDto User { get; set; }
    }
}