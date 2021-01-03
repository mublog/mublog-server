using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<PagedList<Post>> GetPaged(PostQueryParameters queryParameters, Profile profile = null);
        Task<Post> FindByPublicId(int publicId, Profile profile = null);
        Task<bool> AddLike(Post post, Profile profile);
        Task<bool> RemoveLike(Post post, Profile profile);
        Task<bool> ChangeContent(Post post);
    }
}