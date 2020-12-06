using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        PagedList<Post> GetPaged(QueryParameters queryParameters);
        PagedList<PostWithLike> GetPagedWithLikes(QueryParameters queryParameters, Profile user = null);
        Task<PostWithLike> GetByPublicId(int id, Profile user);
        Task<PostWithLike> GetByPublicId(int id, string username = null);
        Task<Post> GetByUserId(int id);
        Task<bool> AddLike(Post post, Profile user);
        Task<bool> RemoveLike(Post post, Profile user);
    }
}