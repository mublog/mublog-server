using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data
{
    public class PostQueryParameters : QueryParameters
    {
        public Profile Profile { get; set; } = null;
    }
}