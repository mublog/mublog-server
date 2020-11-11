using System.Collections.Generic;

namespace Mublog.Server.Domain.Entities
{
    public class Post : BaseEntity
    {
        public int PublicId { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<Media> Mediae { get; set; }
        public ICollection<User> Likes { get; set; }
    }
}