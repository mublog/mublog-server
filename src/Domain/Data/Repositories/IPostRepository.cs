using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        PagedList<PostWithLike> GetPagedWithLikes(PostQueryParameters queryParameters, Profile user = null);
        Task<PostWithLike> GetByPublicId(int id, string username = null);
        Task<bool> AddLike(Post post, Profile user);
        Task<bool> RemoveLike(Post post, Profile user);
    }
}