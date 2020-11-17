using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetByPublicId(int id);
        Task<Post> GetByUserId(int id);
    }
}