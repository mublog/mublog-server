using System.Collections.Generic;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public Media ProfileImage { get; set; }
        public Media HeaderImage { get; set; }
        public UserState UserState { get; set; }
        public List<Post> Posts { get; set; }
        public List<Post> LikedPosts { get; set; }
        public List<Media> Mediae { get; set; }
    }
}