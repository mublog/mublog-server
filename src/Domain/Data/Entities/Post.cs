using System;
using System.Collections.Generic;

namespace Mublog.Server.Domain.Data.Entities
{
    public class Post : BaseEntity
    {
        public int PublicId { get; set; }
        public string Content { get; set; }
        public DateTime PostEditedDate { get; set; }
        public int OwnerId { get; set; }
        public Profile Owner { get; set; }
        public ICollection<PostImage> Mediae { get; set; }
        public ICollection<Profile> Likes { get; set; }
    }
}