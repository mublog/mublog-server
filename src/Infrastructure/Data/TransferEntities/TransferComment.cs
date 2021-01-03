using System;
using Mublog.Server.Domain.Data.Entities;
using NpgsqlTypes;

namespace Mublog.Server.Infrastructure.Data.TransferEntities
{
    public class TransferComment
    {
        public long Id { get; set; }
        public NpgsqlDateTime CreatedDate { get; set; }
        public NpgsqlDateTime UpdatedDate { get; set; }
        public string Content { get; set; }
        public long ParentPostId { get; set; }
        public int OwnerId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public long ProfileImageId { get; set; }
        public Guid ProfileImagePublicId { get; set; }

        public Comment ToComment()
        {
            var profileImage = new Media
            {
                Id = ProfileImageId,
                PublicId = ProfileImagePublicId,
                OwnerId = OwnerId
            };

            var profile = new Profile
            {
                Id = OwnerId,
                Username = Username,
                DisplayName = DisplayName,
                ProfileImageId = ProfileImageId,
                ProfileImage = profileImage
            };

            var comment = new Comment
            {
                Id = Id,
                CreatedDate = CreatedDate.ToDateTime(),
                UpdatedDate = UpdatedDate.ToDateTime(),
                Content = Content,
                ParentPostId = ParentPostId,
                OwnerId = OwnerId,
                Owner = profile
            };

            return comment;
        }
    }
}