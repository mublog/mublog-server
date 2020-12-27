using System;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;
using NpgsqlTypes;

namespace Mublog.Server.Infrastructure.Common.Helpers
{
    public class PGPostEntity
    {
        public int Id { get; set; }
        public NpgsqlDateTime CreatedDate { get; set; }
        public NpgsqlDateTime UpdatedDate { get; set; }
        public int PublicId { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; set; }
        public NpgsqlDateTime PostEditedDate { get; set; }

        public Post ToPost => new Post
        {
            Id = Id,
            CreatedDate = CreatedDate.ToDateTime(),
            UpdatedDate = UpdatedDate.ToDateTime(),
            PublicId = PublicId,
            Content = Content,
            OwnerId = OwnerId,
            PostEditedDate = PostEditedDate.ToDateTime()
        };
        
        public PostWithLike ToPostWithLike => new PostWithLike
        {
            Id = Id,
            CreatedDate = CreatedDate.ToDateTime(),
            UpdatedDate = UpdatedDate.ToDateTime(),
            PublicId = PublicId,
            Content = Content,
            OwnerId = OwnerId,
            PostEditedDate = PostEditedDate.ToDateTime(),
            Liked = false
        };
    }
}