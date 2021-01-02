using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public class Account : BaseEntity
    {
        public string Email { get; set; }
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}