using System.Collections.Generic;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int ProfileImageId { get; set; }
        public Media ProfileImage { get; set; }
        public int HeaderImageId { get; set; }
        public Media HeaderImage { get; set; }
        public UserState UserState { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Post> LikedPosts { get; set; }
        public ICollection<Media> Mediae { get; set; }
    }
}