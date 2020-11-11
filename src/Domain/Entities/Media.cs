using System;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Entities
{
    public class Media : BaseEntity
    {
        public Guid PublicId { get; set; }
        public MediaType MediaType { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int PostId { get; set; }
        public Post ParentPost { get; set; }
    }
}