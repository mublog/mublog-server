using System;

namespace Mublog.Server.Domain.Data.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}