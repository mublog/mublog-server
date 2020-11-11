using System;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Entities
{
    public class Media : BaseEntity
    {
        public Guid PublicId { get; set; }
        public MediaType MediaType { get; set; }
        public User Owner { get; set; }
        public Post ParentPost { get; set; }
    }
}