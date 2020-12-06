using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Common.Helpers
{
    public class PostWithLike : Post
    {
        public bool Liked { get; set; }
    }
}