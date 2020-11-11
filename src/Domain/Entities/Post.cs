using System.Collections.Generic;

namespace Mublog.Server.Domain.Entities
{
    public class Post : BaseEntity
    {
        public int PublicId { get; set; }
        public string Content { get; set; }
        public User Owner { get; set; }
        public List<Media> Mediae { get; set; }
        public List<User> Likes { get; set; }
    }
}