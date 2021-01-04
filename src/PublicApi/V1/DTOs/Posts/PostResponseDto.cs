namespace Mublog.Server.PublicApi.V1.DTOs.Posts
{
    public class PostResponseDto
    {
        public long Id { get; set; }
        public string TextContent { get; set; }
        public long DatePosted { get; set; }
        public long DateEdited { get; set; }
        public long LikeAmount { get; set; }
        public long CommentsAmount { get; set; }
        public bool Liked { get; set; }
        public PostUserResponseDto User { get; set; }
    }
}