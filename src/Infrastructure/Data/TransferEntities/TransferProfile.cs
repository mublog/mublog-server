using System;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Enums;
using NpgsqlTypes;

namespace Mublog.Server.Infrastructure.Data.TransferEntities
{
    public class TransferProfile
    {
        public long Id { get; set; }
        public NpgsqlDateTime CreatedDate { get; set; }
        public NpgsqlDateTime UpdatedDate { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public long ProfileImageId { get; set; }
        public Guid ProfileImagePublicId { get; set; }
        public UserState UserState { get; set; }

        
        public Profile ToProfile()
        {
            var profileImage = new Media
            {
                Id = ProfileImageId, 
                PublicId = ProfileImagePublicId
            };
            
            var profile = new Profile
            {
                Id = Id,
                CreatedDate = CreatedDate.ToDateTime(),
                UpdatedDate = UpdatedDate.ToDateTime(),
                DisplayName = DisplayName,
                Username = Username,
                Description = Description,
                ProfileImageId = ProfileImageId,
                ProfileImage = new Media{Id = ProfileImageId, PublicId = ProfileImagePublicId},
                UserState = UserState
            };
            
            return profile;
        }
    }
}