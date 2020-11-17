using System;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Domain.Data.Entities
{
    public abstract class BaseImageEntity : BaseEntity
    {
        public Guid PublicId { get; set; }
        public MediaType MediaType { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}