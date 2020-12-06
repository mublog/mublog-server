using System;

namespace Mublog.Server.PublicApi.Common.DTOs.V1.Posts
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public long DatePosted { get; set; }
        public long DateEdited { get; set; }
        public int LikeAmount { get; set; }
        public bool Liked { get; set; }
        public PostUserResponseDto User { get; set; }
    }
}