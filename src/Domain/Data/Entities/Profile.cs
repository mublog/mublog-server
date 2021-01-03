using System.Collections.Generic;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Data.Entities
{
    public class Profile : BaseEntity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public long ProfileImageId { get; set; }
        public Media ProfileImage { get; set; }
        public UserState UserState { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Post> LikedPosts { get; set; }
        public ICollection<Media> Mediae { get; set; }
        public ICollection<Profile> Followers { get; set; }
        public long FollowerCount { get; set; }
        public ICollection<Profile> Following { get; set; }
        public long FollowingCount { get; set; }
        public bool FollowStatus { get; set; }    // Should not be in Domain 
    }
}