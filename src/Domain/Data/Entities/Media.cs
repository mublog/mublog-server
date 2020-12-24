using System;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Data.Entities
{
    public class Media : BaseEntity
    {
        public Guid PublicId { get; set; }
        public MediaType MediaType { get; set; }
        public int OwnerId { get; set; }
        public Profile Owner { get; set; }
        public int PostId { get; set; }
        public Post ParentPost { get; set; }
    }
}