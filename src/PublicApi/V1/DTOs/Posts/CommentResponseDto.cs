using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Common.Helpers;

namespace Mublog.Server.PublicApi.V1.DTOs.Posts
{
    public class CommentResponseDto
    {
        public long Id { get; set; }
        public string TextContent { get; set; }
        public long DatePosted { get; set; }
        public PostUserResponseDto User { get; set; }

        
        public CommentResponseDto(Comment comment)
        {
            Id = comment.Id;
            TextContent = comment.Content;
            DatePosted = comment.CreatedDate.ToUnixTimestamp();
            User = new PostUserResponseDto(comment.Owner);
        }
        
        public CommentResponseDto()
        {
            
        }
    }
}